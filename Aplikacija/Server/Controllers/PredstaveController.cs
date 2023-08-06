using Microsoft.AspNetCore.Mvc;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class PredstaveController : ControllerBase
{
    public Context Context {get; set; }

    public PredstaveController (Context context)
    {
        Context=context;
    }
    

    [HttpGet("PreuzmiSvePredstave")]
     public async Task<ActionResult> PreuzmiSvePredstave()
    {
        var podaci = (await Context
            .Predstave
            .Include(p=>p.Glumci)
            .Where(p=>p.DatumVreme>DateTime.Now)
            .Include(p=>p.Organizator)
            .Where(p=>p.Organizator!=null)
            .ToListAsync());
       // var grupisano = podaci
          //  .GroupBy(p => p.Naziv);
         var kate="Pozorišne predstave";
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
                                r.BrMesta,
                                r.Glumci,
                                r.KratakOpis,
                                kate
                              // r.Organizator
                            }).ToList());
    }
    
    [HttpGet("PreuzmiPodatkeOPredstavi/{idPredstave}")]
     public async Task<ActionResult> PreuzmiPodatkeOPredstavi(int idPredstave)
    {
       
          var podaci = await Context.Predstave.Include(p=>p.Glumci).FirstOrDefaultAsync(p=>p.ID==idPredstave);
          
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
                             //  r.Organizator
                            }));
          }
          else
          {
            return BadRequest("Neuspelo");
          }
    }
   
    [HttpGet("PreuzmiPredstaveOrganizatora/{idOrganizatora}")]
    public async Task<ActionResult> PreuzmiPredstaveOrganizatora(int idOrganizatora)
    {
        var dogadjaji = await Context
        .Predstave
        .Where(p=>p.Organizator.ID==idOrganizatora)
        .Include(p=>p.Glumci)
            .ToListAsync();
        

        return Ok(dogadjaji.
           
                            Select(r => new 
                            {
                                 r.ID,
                                r.Naziv,
                                r.DatumVreme,
                                r.Mesto,
                                r.CenaKarte,
                                r.BrMesta,
                                r.Glumci,
                                r.KratakOpis
                               //r.Organizator
                            }).ToList());
    }
   
   [HttpPost("UpisiPredstavu/{idOrganizatora}/{naziv}/{datumVreme}/{mesto}/{opis}/{glumci}/{brmesta}/{cenaUlaza}")]
    public async Task<ActionResult> UpisiPredstavu(int idOrganizatora,string naziv,string datumVreme,string mesto,int cenaUlaza,string opis,string glumci,
    int brmesta)
    {
         var organizator = await Context.Korisnici.FindAsync(idOrganizatora);
        //var organizator =await Context.Korisnici.Where(p=> p.Email==email).FirstOrDefaultAsync();
        var film=new Predstave();
         film.Naziv=naziv;
         film.DatumVreme=DateTime.Parse(datumVreme);
         film.BrMesta=brmesta;
         film.CenaKarte=cenaUlaza;
         film.KratakOpis=opis;
         film.Mesto=mesto;
        if(organizator!=null && (organizator.TipKorisnika=="Organizator"|| organizator.TipKorisnika=="Administrator"))
        {
            if(organizator.OrganizovanePredstave==null){
                organizator.OrganizovanePredstave=new List<Predstave>();
                organizator.OrganizovanePredstave?.Add(film);
            }else{
                organizator.OrganizovanePredstave?.Add(film);
            }
            //organizator.OrganizovanePredstave?.Add(film);
            //ne ynam da li treba na ovaj nacin
            film.Organizator=organizator;
            await Context.Predstave.AddAsync(film);
            await Context.SaveChangesAsync();
            return Ok($"Upisana je predstava sa ID: {film.ID}.");
        }
        else
        {
            return BadRequest("Nije upisana");
        }
    }
   
    [HttpPut("IzmeniPredstavu/{id}")]
    public async Task<ActionResult> IzmeniPredstavu(int id, [FromBody]Predstave predstava)
    {
        var stara = await Context.Predstave.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(predstava.Naziv) )
        {
            stara.Naziv = predstava.Naziv;
           
            stara.CenaKarte = predstava.CenaKarte;
            stara.DatumVreme =predstava.DatumVreme;
            stara.Mesto = predstava.Mesto;
            stara.BrMesta =predstava.BrMesta;
            stara.KratakOpis = predstava.KratakOpis;
           // stara.Organizator=predstava.Organizator;
           

            Context.Predstave.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjena predstava ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

    [HttpDelete("IzbrisiPredstavu/{id}")]
    public async Task<ActionResult> IzbrisatiPredstavu(int id)
    {
        Predstave stara;
       if((stara=Context.Predstave.Include(p=>p.Glumci).Where(r=>r.ID==id).FirstOrDefault())!=null)
        {
    	
         if (stara.Glumci!=null)
         {
            string naziv = stara.Naziv;
           foreach (Glumci g in stara.Glumci)
            {
                Context.Glumci.Remove(g);
            }
            Context.Predstave.Remove(stara);
            
            await Context.SaveChangesAsync();
            return Ok($"Izbrisana predastava sa nazivom={naziv}");
        }
        else
        {
            string naziv = stara.Naziv;
            Context.Predstave.Remove(stara);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisana predstava sa nazivom={naziv}"); 
        }
        }
        else
        {
              return BadRequest("Neuspelo!");
        }
    }
    
}