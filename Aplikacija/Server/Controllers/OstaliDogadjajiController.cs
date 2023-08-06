using Microsoft.AspNetCore.Mvc;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class OstaliDogadjajiController : ControllerBase
{
    public Context Context {get; set; }

    public OstaliDogadjajiController (Context context)
    {
        Context=context;
    }

     [HttpGet("PreuzmiSveOstaleDogadjaje")]
     public async Task<ActionResult> PreuzmiSveOstaleDogadjaje()
    {
        var podaci = (await Context
            .OstaliDogadjaji
            .Where(p=>p.TrajeDo>DateTime.Now)
            .Include(p=>p.Organizator)
            .Where(p=>p.Organizator!=null)
            .ToListAsync());
    

            
     var kate="Ostali događaji";
         

        return Ok(podaci.
            
                    Select(r => new 
                            {
                                r.ID,
                                r.Naziv,
                                r.DatumVreme,
                                r.DatumVreme.Day,
                                r.DatumVreme.Month,
                                r.DatumVreme.Year,
                                r.DatumVreme.TimeOfDay.Hours,
                                r.DatumVreme.TimeOfDay.Minutes,
                                r.Mesto,
                                r.CenaKarte,
                                r.KratakOpis,
                                r.TrajeDo,
                                kate
                             //  r.Organizator
                            }).ToList());
    }
    
    [HttpGet("PreuzmiPodatkeOOstalomDogadjaju/{idDogadjaja}")]
     public async Task<ActionResult> PreuzmiPodatkeOOstalomDogadjaju(int idDogadjaja)
    {
          var podaci = await Context.OstaliDogadjaji.FindAsync(idDogadjaja);
          if(podaci!=null)
          {
               return Ok(
                          ( new 
                            {
                                podaci.ID,
                                podaci.Naziv,
                                podaci.DatumVreme,
                                podaci.DatumVreme.Day,
                                podaci.DatumVreme.Month,
                                podaci.DatumVreme.Year,
                                podaci.DatumVreme.TimeOfDay.Hours,
                                podaci.DatumVreme.TimeOfDay.Minutes,
                                podaci.Mesto,
                                podaci.CenaKarte,
                                podaci.KratakOpis,
                                podaci.TrajeDo
                             //  r.Organizator
                            }));
          }
          else
          {
            return Ok("Neuspelo");
          }
    }

    [HttpGet("PreuzmiOstaleDogadjajeOrganiztora/{idOrganizatora}")]
    public async Task<ActionResult> PreuzmiOstaleDogadjajeOrganizatora(int idOrganizatora)
    {
        var dogadjaji = await Context
        .OstaliDogadjaji
        .Where(p=>p.Organizator.ID==idOrganizatora)
            .ToListAsync();
        

        return Ok(dogadjaji.
           
                            Select(r => new 
                            {
                                r.ID,
                                r.Naziv,
                                r.DatumVreme,
                                r.Mesto,
                                r.CenaKarte,
                                r.KratakOpis,
                                r.TrajeDo
                               //r.Organizator
                            }).ToList());
    }
   
    [HttpPost("UpisiOstalidogadjaj/{idOrganizatora}/{naziv}/{datumVreme}/{mesto}/{opis}/{trajedo}/{cenaUlaza}")]
    public async Task<ActionResult> UpisiOstaiDogadjaj(int idOrganizatora,string naziv,string datumVreme,string mesto,int cenaUlaza,string opis,string trajedo)
    {
        var organizator = await Context.Korisnici.FindAsync(idOrganizatora);
       // var organizator =await Context.Korisnici.Where(p=> p.Email==email).FirstOrDefaultAsync();
        var film=new OstaliDogadjaji();
          film.Naziv=naziv;
         film.DatumVreme=DateTime.Parse(datumVreme);
         film.CenaKarte=cenaUlaza;
         film.KratakOpis=opis;
         film.TrajeDo=DateTime.Parse(trajedo);
         film.Mesto=mesto;

        if(organizator!=null && (organizator.TipKorisnika=="Organizator"|| organizator.TipKorisnika=="Administrator"))
        {
            if(organizator.OrganizovaniOstaliDogadjaji==null){
                organizator.OrganizovaniOstaliDogadjaji=new List<OstaliDogadjaji>();
                organizator.OrganizovaniOstaliDogadjaji?.Add(film);
            }else{
                organizator.OrganizovaniOstaliDogadjaji?.Add(film);
            }
            //organizator.OrganizovaniOstaliDogadjaji?.Add(film);
            
            //ne ynam da li treba na ovaj nacin
            film.Organizator=organizator;
            await Context.OstaliDogadjaji.AddAsync(film);
            await Context.SaveChangesAsync();
            return Ok($"Upisan je dogadjaj sa ID: {film.ID}.");
        }
        else
        {
            return BadRequest("Nije upisan");
        }
    }
   
    [HttpPut("IzmeniOstaliDogadjaj/{id}")]
    public async Task<ActionResult> IzmeniOstaliDogadjaj(int id, [FromBody]OstaliDogadjaji dogadjaj)
    {
        var stara = await Context.OstaliDogadjaji.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(dogadjaj.Naziv) )
        {
            stara.Naziv = dogadjaj.Naziv;
            stara.CenaKarte = dogadjaj.CenaKarte;
            stara.DatumVreme =dogadjaj.DatumVreme;
            stara.Mesto = dogadjaj.Mesto;
            stara.TrajeDo =dogadjaj.TrajeDo;
            stara.KratakOpis = dogadjaj.KratakOpis;
           // stara.Organizator =dogadjaj.Organizator;

            Context.OstaliDogadjaji.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjen dogadjaj ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

    [HttpDelete("IzbrisiOstaliDogadjaj/{id}")]
    public async Task<ActionResult> IzbrisatiOstalidogadjaj(int id)
    {
        var stara = await Context.OstaliDogadjaji.FindAsync(id);

        if (stara != null)
        {
            string naziv = stara.Naziv;
            Context.OstaliDogadjaji.Remove(stara);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan dogadjaj sa nazivom={naziv}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }
    

}