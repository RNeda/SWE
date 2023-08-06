using Microsoft.AspNetCore.Mvc;
using Models;


namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmoviController : ControllerBase
{
    public Context Context {get; set; }

    public FilmoviController (Context context)
    {
        Context=context;
    }


    [HttpGet("PreuzmiSveFilmove")]
    public async Task<ActionResult> PreuzmiSveFilmove()
    {
        var podaci = (await Context
            .Filmovi
            .Include(p=>p.Glumci)
            .Include(p=>p.Organizator)
            .Where(p=>p.Organizator!=null)
             .Where(p=>p.DatumVreme>DateTime.Now)
            .ToListAsync());
       // var grupisano = podaci
          //  .GroupBy(p => p.Naziv);
     var kate="Film";
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
                                r.Zanr,
                                r.Mesto,
                                r.CenaKarte,
                                r.VremeTrajanjaUMin,
                                r.Glumci,
                                r.KratakOpis,
                                r.BrMesta,
                                kate
                                //r.Organizator
                            }).ToList());
    }
   
   [HttpGet("PreuzmiPodatkeOFilmu/{idFilma}")]
     public async Task<ActionResult> PreuzmiPodatkeOFilmu(int idFilma)
    {
          var podaci = await Context.Filmovi.Include(p=>p.Glumci).FirstOrDefaultAsync(p=>p.ID==idFilma);
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
                                podaci.BrMesta,
                                podaci.Glumci,
                                podaci.KratakOpis,
                                podaci.Zanr,
                                podaci.VremeTrajanjaUMin,
                             //  r.Organizator
                            }));
          }
          else
          {
            return BadRequest("Neuspelo");
          }
    }

   [HttpGet("PreuzmiFilmoveOrganiztora/{idOrganizatora}")]
    public async Task<ActionResult> PreuzmiFilmoveOrganizatora(int idOrganizatora)
    {
        var filmovi = await Context
        .Filmovi
        .Where(p=>p.Organizator.ID==idOrganizatora)
        .Include(p=>p.Glumci)
            .ToListAsync();
        

        return Ok(filmovi.
           
                            Select(r => new 
                            {
                                r.ID,
                                r.Naziv,
                                r.DatumVreme,
                                r.Zanr,
                                r.Mesto,
                                r.CenaKarte,
                                r.VremeTrajanjaUMin,
                                r.Glumci,
                                r.KratakOpis,
                                //r.Organizator
                            }).ToList());
    }
 
 [HttpPost("UpisiFilm/{idOrganizatora}/{naziv}/{datumVreme}/{mesto}/{opis}/{glumci}/{trajanje}/{zanr}/{brmesta}/{cenaUlaza}")]
    public async Task<ActionResult> UpisiFilm(int idOrganizatora,string naziv,string datumVreme,string mesto,int cenaUlaza,string opis,string glumci,
    int trajanje,string zanr,int brmesta)
    {
         var organizator = await Context.Korisnici.FindAsync(idOrganizatora);
         var film=new Filmovi();
         film.Naziv=naziv;
         film.DatumVreme=DateTime.Parse(datumVreme);
         film.BrMesta=brmesta;
         film.CenaKarte=cenaUlaza;
         film.KratakOpis=opis;
         film.Mesto=mesto;
         film.VremeTrajanjaUMin=trajanje;
         film.Zanr=zanr;

        //var organizator =await Context.Korisnici.Where(p=> p.Email==email).FirstOrDefaultAsync();
        if(organizator!=null && (organizator.TipKorisnika=="Organizator"|| organizator.TipKorisnika=="Administrator"))
        {
            if(organizator.OrganizovaniFilmovi==null){
                organizator.OrganizovaniFilmovi=new List<Filmovi>();
                organizator.OrganizovaniFilmovi?.Add(film);
            }else{
                organizator.OrganizovaniFilmovi?.Add(film);
            }
            //organizator.OrganizovaniFilmovi?.Add(film);
            //ne ynam da li treba na ovaj nacin
            film.Organizator=organizator;
            await Context.Filmovi.AddAsync(film);
            await Context.SaveChangesAsync();
            return Ok($"Upisan je film sa ID: {film.ID}.");
        }
        else
        {
            return BadRequest("Nije upisan");
        }
    }



   /* [HttpPost("UpisiFilm")]
    public async Task<ActionResult> UpisiFilm([FromBody]Filmovi film)
    {
        try
        {
            await Context.Filmovi.AddAsync(film);
            await Context.SaveChangesAsync();
            return Ok($"ID novog filma je = {film.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }*/

    [HttpPut("IzmeniFilm/{id}")]
    public async Task<ActionResult> IzmeniFilm(int id, [FromBody]Filmovi film)
    {
        var stara = await Context.Filmovi.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(film.Naziv) )
        {
            stara.Naziv = film.Naziv;
            stara.BrMesta =film.BrMesta;
            stara.CenaKarte = film.CenaKarte;
            stara.DatumVreme =film.DatumVreme;
            stara.Glumci = film.Glumci;
            stara.KratakOpis =film.KratakOpis;
            stara.Mesto = film.Mesto;
            stara.Organizator =film.Organizator;
            stara.VremeTrajanjaUMin= film.VremeTrajanjaUMin;
            stara.Zanr =film.Zanr;

            Context.Filmovi.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjen film ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

    [HttpDelete("IzbrisiFilm/{id}")]
    public async Task<ActionResult> IzbrisatiFilm(int id)
    {
        
        Filmovi stara;
       if((stara=Context.Filmovi.Include(p=>p.Glumci).Where(r=>r.ID==id).FirstOrDefault())!=null)
        {
    	
         if (stara.Glumci!=null && stara.Naziv!=null)
         {
            string naziv = stara.Naziv;
           foreach (Glumci g in stara.Glumci)
            {
                Context.Glumci.Remove(g);
            }
            Context.Filmovi.Remove(stara);
            
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan film sa nazivom={naziv}");
        }
        else
        {
            string naziv = stara.Naziv;
            Context.Filmovi.Remove(stara);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan film sa nazivom={naziv}"); 
        }
        }
        else
        {
              return BadRequest("Neuspelo!");
        }
    }


}