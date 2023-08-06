using Microsoft.AspNetCore.Mvc;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class UtakmiceController : ControllerBase
{
    public Context Context {get; set; }

    public UtakmiceController (Context context)
    {
        Context=context;
    }

    [HttpGet("PreuzmiPodatkeOUtakmici/{idUtakmice}")]
     public async Task<ActionResult> PreuzmiPodatkeOUtakmici(int idUtakmice)
    {
          var podaci = await Context.Utakmice.FindAsync(idUtakmice);
          
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
                                podaci.Klub1,
                                podaci.Klub2,
                                podaci.Sport,
                             //  r.Organizator
                            }));
          }
          else
          {
            return BadRequest("Neuspelo");
          }
    }

    
    [HttpGet("PreuzmiSveUtakmice")]
     public async Task<ActionResult> PreuzmiSveUtakmice()
    {
        var podaci = (await Context
            .Utakmice
            .Where(p=>p.DatumVreme>DateTime.Now)
            .Include(p=>p.Organizator)
            .Where(p=>p.Organizator!=null)
            .ToListAsync());
       // var grupisano = podaci
          //  .GroupBy(p => p.Naziv);
var kate="Sport";
        return Ok(podaci.
            
                    Select(r => new 
                            {
                                r.ID,
                                r.Naziv,
                                r.DatumVreme,
                                r.DatumVreme.Day,
                                r.DatumVreme.Month,
                                r.DatumVreme.Year,
                                r.DatumVreme.Hour,
                                r.DatumVreme.Minute,
                                r.Mesto,
                                r.CenaKarte,
                                r.BrMesta,
                                r.Klub1,
                                r.Klub2,
                                r.Sport,
                                kate
                             //  r.Organizator
                            }).ToList());
    }
    [HttpGet("PreuzmiUtakmiceOrganizatora/{idOrganizatora}")]  //radi
   public async Task<ActionResult> PreuzmiUtakmiceOrganizatora(int idOrganizatora)
    {
        try{
        var dogadjaji = await Context
        .Utakmice
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
                                r.BrMesta,
                                r.Klub1,
                                r.Klub2,
                                r.Sport
                               //r.Organizator
                            }).ToList());
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PreuzmiUtakmiceTima/{tim}/{sport}")]  //radi
   public async Task<ActionResult> PreuzmiUtakmiceTima(string tim, string sport)
    {
        try{
        var dogadjaji = await Context
        .Utakmice
        .Where(p=>((p.Klub1==tim)||(p.Klub2==tim))&& p.Sport==sport)
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
                                r.Klub1,
                                r.Klub2
                               //r.Organizator
                            }).ToList());
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PreuzmiUtakmiceSport/{sport}")]  //radi
   public async Task<ActionResult> PreuzmiUtakmiceSport( string sport)
    {
        try{
        var dogadjaji = await Context
        .Utakmice
        .Where(p=> p.Sport==sport)
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
                                r.Klub1,
                                r.Klub2
                               
                               //r.Organizator
                            }).ToList());
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("UpisiUtakmicu/{idOrganizatora}/{naziv}/{datumVreme}/{mesto}/{sport}/{klub1}/{klub2}/{brmesta}/{cenaUlaza}")]
    public async Task<ActionResult> UpisiUtakmicu(int idOrganizatora,string naziv,string datumVreme,string mesto,int cenaUlaza,string sport,string klub1,string klub2,int brmesta)
    {
         var organizator = await Context.Korisnici.FindAsync(idOrganizatora);
         var film=new Utakmice();
          film.Naziv=naziv;
         film.DatumVreme=DateTime.Parse(datumVreme);
         film.BrMesta=brmesta;
         film.CenaKarte=cenaUlaza;
         film.Sport=sport;
         film.Klub1=klub1;
         film.Klub2=klub2;
         film.Mesto=mesto;
        if(organizator!=null && (organizator.TipKorisnika=="Organizator"|| organizator.TipKorisnika=="Administrator"))
        {
             if(organizator.OrganizovaneUtakmice==null){
                organizator.OrganizovaneUtakmice=new List<Utakmice>();
                organizator.OrganizovaneUtakmice?.Add(film);
            }else{
                organizator.OrganizovaneUtakmice?.Add(film);
            }
            //organizator.OrganizovaneUtakmice?.Add(film);
            //ne ynam da li treba na ovaj nacin
            film.Organizator=organizator;
            await Context.Utakmice.AddAsync(film);
            await Context.SaveChangesAsync();
            return Ok($"Upisana je utakmica sa ID: {film.ID}.");
        }
        else
        {
            return BadRequest("Nije upisana");
        }
    }
    [HttpPut("IzmeniUtakmicu/{id}")]  //radi
    public async Task<ActionResult> IzmeniUtakmicu(int id, [FromBody]Utakmice utakmica)
    {
        try{
        var stara = await Context.Utakmice.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(utakmica.Naziv) )
        {
            if(utakmica.Naziv!="string")
            {
                stara.Naziv = utakmica.Naziv;
            }
            if(utakmica.CenaKarte!=0)
            {
                stara.CenaKarte = utakmica.CenaKarte;
            }
            if(utakmica.DatumVreme!=DateTime.Today)
            {
                stara.DatumVreme =utakmica.DatumVreme;
            }
            if(utakmica.Mesto!="string")
            {
                stara.Mesto = utakmica.Mesto;
            }
            if(utakmica.BrMesta!=0)
            {
                stara.BrMesta =utakmica.BrMesta;
            }
            if(utakmica.Sport!="string")
            {
                stara.Sport = utakmica.Sport;
            }
            if(utakmica.Klub1!="string")
            {
                stara.Klub1=utakmica.Klub1;
            }
            if(utakmica.Klub2!="string")
            {
                stara.Klub2=utakmica.Klub2;
            }
           // stara.Organizator=utakmica.Organizator;
           
            Context.Utakmice.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjena utakmica ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("IzbrisiUtakmicu/{id}")]  //ne radi
    public async Task<ActionResult> IzbrisatiUtakmicu(int id)
    {
        
        try{
        var stara = await Context.Utakmice.FindAsync(id);
        
        if (stara != null)
        {
            //var org = stara.Organizator;
            string naziv = stara.Naziv;
            //org.OrganizovaneUtakmice.Remove(stara);
            Context.Utakmice.Remove(stara);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisana utakmica sa nazivom={naziv}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }

       
    }

}