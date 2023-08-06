using Microsoft.AspNetCore.Mvc;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class KoncertiController : ControllerBase
{
    public Context Context {get; set; }

    public KoncertiController (Context context)
    {
        Context=context;
    }

    

    [HttpPost("DodajGostaNaKoncert/{idKonc}")] //radi
    public async Task<ActionResult> DodajGostaNaKoncert(int idKonc, [FromBody]Gosti gost)
    {
        try
        {
          
            var koncert = Context.Koncerti.Include(p=>p.Gosti).Where(r=>r.ID==idKonc).FirstOrDefault();
            
            if( koncert ==null){
                return BadRequest("ne postoji koncert sa tim id");

            }
            else{
                
                var nov = new Gosti();
                nov=gost;
                Context.Koncerti.Find(idKonc).Gosti.Add(nov);
               
            }
            
            //await Context.Koncerti.AddAsync(konc);
            await Context.SaveChangesAsync();
            return Ok($"dodat gost sa id {gost.ID}u konceert {koncert.ID}");
           
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

  [HttpGet("PreuzmiSveKoncerte")]
     public async Task<ActionResult> PreuzmiSveKoncerte()
    {
        var podaci = (await Context
            .Koncerti
            .Include(p=>p.Gosti)
            .Where(p=>p.DatumVreme>DateTime.Now)
            .Include(p=>p.Organizator)
            .Where(p=>p.Organizator!=null)
            .ToListAsync());
       // var grupisano = podaci
          //  .GroupBy(p => p.Naziv);
      var kate="Koncerti";
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
                                r.Gosti,
                                r.Izvodjac,
                                kate
                               //r.Organizator
                            }).ToList());
    }
    [HttpGet("PreuzmiKoncerteOrganiztora/{idOrganizatora}")]//radi
    public async Task<ActionResult> PreuzmiKoncerteOrganizatora(int idOrganizatora)
    {
        var koncerti = await Context
        .Koncerti
        .Where(p=>p.Organizator.ID==idOrganizatora)
        .ToListAsync();
        

        return Ok(koncerti.
           
                            Select(r => new 
                            {
                                r.ID,
                                r.Naziv,
                                r.DatumVreme,
                                r.Mesto,
                                r.CenaKarte,
                                r.BrMesta,
                                r.Gosti,
                                r.Izvodjac,
                               //r.Organizator
                            }).ToList());
    }
   
    [HttpPost("UpisiKoncert/{idOrganizatora}/{naziv}/{datumVreme}/{mesto}/{izvodjac}/{gosti}/{brmesta}/{cenaUlaza}")]
    public async Task<ActionResult> UpisiKoncert(int idOrganizatora,string naziv,string datumVreme,string mesto,int cenaUlaza,string izvodjac,string gosti,int brmesta)
    {
         var organizator = await Context.Korisnici.FindAsync(idOrganizatora);
         var film=new Koncerti();
         film.Naziv=naziv;
         film.DatumVreme=DateTime.Parse(datumVreme);
         film.BrMesta=brmesta;
         film.CenaKarte=cenaUlaza;
         film.Izvodjac=izvodjac;
         film.Mesto=mesto;

        //var organizator =await Context.Korisnici.Where(p=> p.Email==email).FirstOrDefaultAsync();
        if(organizator!=null && (organizator.TipKorisnika=="Organizator"|| organizator.TipKorisnika=="Administrator"))
        {
            if(organizator.OrganizovaniKoncerti==null){
                organizator.OrganizovaniKoncerti=new List<Koncerti>();
                organizator.OrganizovaniKoncerti?.Add(film);
            }else{
                organizator.OrganizovaniKoncerti?.Add(film);
            }
            //organizator.OrganizovaniKoncerti?.Add(film);
            //ne ynam da li treba na ovaj nacin
            film.Organizator=organizator;
            await Context.Koncerti.AddAsync(film);
            await Context.SaveChangesAsync();
            return Ok($"Upisan je koncert sa ID: {film.ID}.");
        }
        else
        {
            return BadRequest("Nije upisan");
        }
    }
    [HttpPut("IzmeniKoncert/{id}")]   //radi
    public async Task<ActionResult> IzmeniKoncert(int id, [FromBody]Koncerti koncert)
    {
        try{
        var stara = await Context.Koncerti.FindAsync(id);

        if (stara != null)
        {
            if(koncert.Naziv!="string")
            {
                stara.Naziv = koncert.Naziv;
            }
            if(koncert.BrMesta!=0)
            {
                stara.BrMesta =koncert.BrMesta;
            }
            if(koncert.CenaKarte!=0)
            {
               stara.CenaKarte = koncert.CenaKarte;
            }
            if(koncert.DatumVreme!=DateTime.Today)
            {
               stara.DatumVreme =koncert.DatumVreme;
            }
             if(koncert.Izvodjac!="string")
            {
                stara.Izvodjac =koncert.Izvodjac;
            }
             if(koncert.Mesto!="string")
            {
               stara.Mesto = koncert.Mesto;
            }
            
            //stara.Gosti = koncert.Gosti;
            
           // stara.Organizator =koncert.Organizator; 

            Context.Koncerti.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjen koncert ID={stara.ID}");
        }
        else
        {
            return BadRequest("ne posoji taj koncert!");
        }
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("IzbrisiKoncert/{id}")] //radi
    public async Task<ActionResult> IzbrisatiKoncert(int id)
    {
        Koncerti stara;
       if((stara=Context.Koncerti.Include(p=>p.Gosti).Where(r=>r.ID==id).FirstOrDefault())!=null)
        {
    	
         if (stara.Gosti!=null)
       {
            string naziv = stara.Naziv;
           foreach (Gosti g in stara.Gosti)
            {
                Context.Gosti.Remove(g);
            }
            Context.Koncerti.Remove(stara);
            
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan koncert sa nazivom={naziv}");
        }
        else
        {
            string naziv = stara.Naziv;
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan koncert sa nazivom={naziv}");
          
        }
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

    [HttpGet("PreuzmiGosteKoncerta/{idKonc}")]//radi
    public async Task<ActionResult> PreuzmiGosteKoncerta(int idKonc)
    {
       var podaci = (await Context
            .Koncerti
            .Include(p=>p.Gosti)
            .Where(p=>p.ID==idKonc)
            .ToListAsync());
       // var grupisano = podaci
          //  .GroupBy(p => p.Naziv);

        return Ok(podaci.
            
                    Select(r => new 
                            {
                                
                                r.Gosti
                            }).ToList());
    }

    [HttpGet("VratiKoncert/{idKonc}")]//radi
    public async Task<ActionResult> VratiKoncert(int idKonc)
    {
         var r = await Context.Koncerti.Include(p=>p.Gosti).FirstOrDefaultAsync(p=>p.ID==idKonc);
        // var grupisano = podaci
        //  .GroupBy(p => p.Naziv);

        return Ok(
                    new
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
                        r.Gosti,
                        r.Izvodjac,
                        //r.Organizator
                    });
    }
}