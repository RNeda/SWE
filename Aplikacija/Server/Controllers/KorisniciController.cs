using Microsoft.AspNetCore.Mvc;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class KorisniciController : ControllerBase
{
    public Context Context {get; set; }

    public KorisniciController (Context context)
    {
        Context=context;
    }


    [HttpPost("UpisiKorisnika")]  //radi
    public async Task<ActionResult> UpisiKorisnika([FromBody]Korisnici korisnik)
    {
        try
        {
            var k = Context.Korisnici.Where(p=>p.Email==korisnik.Email).FirstOrDefault();
            if(k!=null){
                return BadRequest("vec postoji takav email!");
            }
            

            if(korisnik.TipKorisnika=="korisnik"||korisnik.TipKorisnika=="Korisnik")
            {
                korisnik.Dogadjaji=new List<Dogadjaji>();
            }
            else
            {
                if(korisnik.TipKorisnika=="organizator"||korisnik.TipKorisnika=="Organizator"||korisnik.TipKorisnika=="administrator"||korisnik.TipKorisnika=="Administrator")
                {
                    if(korisnik.TipKorisnika=="administrator"||korisnik.TipKorisnika=="Administrator"){
                        var kor = Context.Korisnici.Where(p=>(p.TipKorisnika=="administrator" || p.TipKorisnika=="Administrator")).FirstOrDefault();
                        if(kor!=null){return BadRequest("vec postoji administrator!");}
                    }
                    korisnik.Dogadjaji=new List<Dogadjaji>();
                    korisnik.OrganizovaniFilmovi=new List<Filmovi>();
                    korisnik.OrganizovanePredstave=new List<Predstave>();
                    korisnik.OrganizovaniKoncerti=new List<Koncerti>();
                    korisnik.OrganizovaneUtakmice=new List<Utakmice>();
                    korisnik.OrganizovaniOstaliDogadjaji=new List<OstaliDogadjaji>();
                }
                else{return BadRequest("nevalidan tip korisnika!");}

            }

            
            await Context.Korisnici.AddAsync(korisnik);
            await Context.SaveChangesAsync();
            return Ok($"Upisan je korisnik sa ID: {korisnik.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost("UpisiKorisnikaNovo/{ime}/{prezime}/{email}/{sifra}")]  //radi
    public async Task<ActionResult> UpisiKorisnikaNovo(string ime, string prezime, string email, string sifra)
    {
        try
        {
            //var k = Context.Korisnici.Where(p=>p.Email==korisnik.Email).FirstOrDefault();
            foreach(Korisnici k in Context.Korisnici)
            {

                if(email==k.Email){
                    return BadRequest("vec postoji takav email!");
                }
            }
            Korisnici korisnik = new Korisnici();
            korisnik.Ime = ime;
            korisnik.Prezime=prezime;
            korisnik.Email=email;
            korisnik.TipKorisnika="Korisnik";
            korisnik.Sifra=sifra;
            korisnik.Dogadjaji=new List<Dogadjaji>();
            korisnik.Dogadjaji=new List<Dogadjaji>();
            korisnik.OrganizovaniFilmovi=new List<Filmovi>();
            korisnik.OrganizovanePredstave=new List<Predstave>();
            korisnik.OrganizovaniKoncerti=new List<Koncerti>();
            korisnik.OrganizovaneUtakmice=new List<Utakmice>();
            korisnik.OrganizovaniOstaliDogadjaji=new List<OstaliDogadjaji>();
           
            await Context.Korisnici.AddAsync(korisnik);
            await Context.SaveChangesAsync();
            return Ok($"Upisan je korisnik sa ID: {korisnik.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("PreuzmiKorisnike")]   //radi
    public async Task<ActionResult> PreuzmiKorisnike() 
    {
        var podaci = (await Context
            .Korisnici
            .ToListAsync());
        

        return Ok(podaci.
                            Select(r => new 
                            {
                                r.ID,
                                r.Ime,
                                r.Prezime,
                                r.TipKorisnika
                            }).ToList());
    }


    [HttpGet("VratiKorisnika/{email}/{sifra}")]   //radi
    public async Task<ActionResult> VratiKorisnika(string email, string sifra) 
    {
        var podaci = (await Context
            .Korisnici
            .Include(p=>p.OrganizovaniKoncerti)
            .Include(p=>p.Dogadjaji)
            .Include(p=>p.OrganizovanePredstave)
            .Include(p=>p.OrganizovaneUtakmice)
            .Include(p=>p.OrganizovaniFilmovi)
            .Include(p=>p.OrganizovaniOstaliDogadjaji)
            .Where(p=>p.Email==email).FirstOrDefaultAsync());
        
        if(podaci!=null){
            if(podaci.Sifra==sifra)
                {return Ok(
                    ( new 
                            {
                                podaci.ID,
                                podaci.Ime,
                                podaci.Prezime,
                                podaci.TipKorisnika,
                                podaci.Sifra,
                                podaci.Dogadjaji,
                                podaci.Email,
                                podaci.OrganizovanePredstave,
                                podaci.OrganizovaneUtakmice,
                                podaci.OrganizovaniFilmovi,
                                podaci.OrganizovaniKoncerti,
                                podaci.OrganizovaniOstaliDogadjaji
                                
                            }));
                }
                
            else{return BadRequest("ne postoji korisnik");}
        }
        else{return BadRequest("ne postoji korisnik");}

    }
    
    [HttpGet("VratiMejl/{idKor}")]   //radi
    public async Task<ActionResult> VratiMejl(int idKor) 
    {
        var kor = (await Context
            .Korisnici
            .Where(p=>p.ID==idKor).FirstOrDefaultAsync());
        
        if(kor!=null){
            return Ok(kor.Email);
        }
        else{
            return BadRequest("ne postoji takav korisnik");
        }
        
    }

    [HttpGet("VratiDogadjajeOrganizatora/{idOrg}")]   //ne radi :(,  vrati prazne liste dogadjaja
    public async Task<ActionResult> VratiDogadjajeOrganizatora(int idOrg) 
    {
        var podaci = ( await Context
            .Korisnici
           /* .Include(p=>p.OrganizovanePredstave)
            .Include(p=>p.OrganizovaneUtakmice)
            .Include(p=>p.OrganizovaniFilmovi)
            .Include(p=>p.OrganizovaniKoncerti)
            .Include(p=>p.OrganizovaniOstaliDogadjaji)*/
            .Where(x=>x.ID==idOrg).ToListAsync());
            
        

        return Ok(podaci.
                            Select(r => new 
                            {
                                r.OrganizovanePredstave,
                                r.OrganizovaneUtakmice,
                                r.OrganizovaniFilmovi,
                                r.OrganizovaniKoncerti,
                                r.OrganizovaniOstaliDogadjaji
                            }).ToList());
    }

    
    [HttpPut("UnaprediUOrganizatora/{id}")]  //radi
    public async Task<ActionResult> UnaprediUOrganizatora(int id)
    {
        var stara = await Context.Korisnici.FindAsync(id);

        if (stara != null)
        {
            stara.TipKorisnika= "Organizator";
            //nisam sigurna da li treba
            stara.OrganizovaniFilmovi=new List<Filmovi>();
            stara.OrganizovanePredstave=new List<Predstave>();
            stara.OrganizovaniKoncerti=new List<Koncerti>();
            stara.OrganizovaneUtakmice=new List<Utakmice>();
            stara.OrganizovaniOstaliDogadjaji=new List<OstaliDogadjaji>();

            Context.Korisnici.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Unapredjen korisnik ID={stara.ID} u organizatora");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }
    [HttpPut("UnaprediUAdministratora/{id}")]  //radi
    public async Task<ActionResult> UnaprediUAdministratora(int id)
    {
       
        var stara = await Context.Korisnici.FindAsync(id);

        if (stara != null)
        {
            var kor = Context.Korisnici.Where(p=>(p.TipKorisnika=="administrator" || p.TipKorisnika=="Administrator")).FirstOrDefault();
            if(kor!=null){return BadRequest("vec postoji administrator!");}
            if(stara.TipKorisnika== "Organizator" || stara.TipKorisnika== "organizator")
            {
                
                stara.TipKorisnika= "Administrator";
            }
            else{
             stara.TipKorisnika= "Administrator";
            stara.OrganizovaniFilmovi=new List<Filmovi>();
            stara.OrganizovanePredstave=new List<Predstave>();
            stara.OrganizovaniKoncerti=new List<Koncerti>();
            stara.OrganizovaneUtakmice=new List<Utakmice>();
            stara.OrganizovaniOstaliDogadjaji=new List<OstaliDogadjaji>();
            }
            Context.Korisnici.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Unapredjen korisnik ID={stara.ID} u administratora");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }
    
    [HttpDelete("IzbrisiKorisnika/{id}")]  //brise samo korisnika ne i dogadjaje :(
    public async Task<ActionResult> IzbrisatiKorisnika(int id)
    {
        var stari = await Context.Korisnici.Include(p=>p.Dogadjaji).Where(x=>x.ID==id).FirstOrDefaultAsync();

        if (stari != null)
        {
            
            string ime = stari.Ime;
            if(stari.TipKorisnika=="Organizator" ||stari.TipKorisnika=="organizator" || stari.TipKorisnika=="Administrator" || stari.TipKorisnika=="administrator")
            {
                
                var obrisi = Context.Korisnici
                    .Include(p=>p.OrganizovanePredstave)
                    .Include(p=>p.OrganizovaneUtakmice)
                    .Include(p=>p.OrganizovaniFilmovi)
                    .Include(p=>p.OrganizovaniKoncerti)
                    .Include(p=>p.OrganizovaniOstaliDogadjaji)
                    .Where(x=>x.ID==id).ToList();

                foreach(var o in obrisi){
                    Context.Remove(o);
                }
                        
            }
            Context.Korisnici.Remove(stari);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan korisnik sa imenom= {ime}");
        
            
           
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

   
         
   
   //ovo se odnosi na izmenu licnih podataka korisnika Ime,Prezime,Sifra
     [HttpPut("IzmeniKorisnika/{id}")]  //radi
    public async Task<ActionResult> IzmeniKorisnika(int id, [FromBody]Korisnici korisnik)
    {
        var stara = await Context.Korisnici.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(korisnik.Email) )
        {
            if(korisnik.Ime!="string")
            {
                stara.Ime=korisnik.Ime;
            }
            if(korisnik.Prezime!="string")
            {
                stara.Prezime=korisnik.Prezime;
            }
            if(korisnik.Sifra!="string")
            {
                stara.Sifra=korisnik.Sifra;
            }
            
            
            Context.Korisnici.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjen korisnik ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }


    [HttpPut("DodajDogadjajOdInteresovanja/{email}/{idDogadjaja}")]  //radi 
    public async Task<ActionResult> DodajDogadjajOdInteresovanja(string email,int idDogadjaja)
    {
        var korisnik = await Context.Korisnici.Include(p=>p.Dogadjaji).FirstOrDefaultAsync(p=>p.Email==email);
        var dogadjajj = await Context.Dogadjaji.FindAsync(idDogadjaja);
      
        if (korisnik != null && dogadjajj!= null)
        {
            if(korisnik.Dogadjaji==null)
            {    
                korisnik.Dogadjaji=new List<Dogadjaji>();
                korisnik.Dogadjaji.Add(dogadjajj);

            Context.Korisnici.Update(korisnik);
            //Context.Dogadjaji.Update(dogadjaj);
            await Context.SaveChangesAsync();
            return Ok($"Dodat {korisnik.Dogadjaji.Count()} dogadjaj {dogadjajj.Naziv} korisniku ID={korisnik.ID}");
            }
            else
            {
                foreach(Dogadjaji d in korisnik.Dogadjaji){
                    if(d==dogadjajj){
                        return Ok("dogadjaj je vec u listi");
                    }
                }
                korisnik.Dogadjaji.Add(dogadjajj);

              Context.Korisnici.Update(korisnik);
              await Context.SaveChangesAsync();
               return Ok($"Dodat {korisnik.Dogadjaji.Count()} dogadjaj {dogadjajj.Naziv} korisniku ID={korisnik.ID}");
            }
        }
        else
        {
            return BadRequest("Neuspelo1!");
        }
    }


    [HttpPut("UkloniDogadjajOdInteresovanja/{idDog}/{email}")]  //radi
    public async Task<ActionResult> UkloniDogadjajOdInteresovanja(int idDog, string email)
    {
        try{
        var korisnik =await Context.Korisnici.Where(p=> p.Email==email).FirstOrDefaultAsync();
        var dogadjajj=await Context.Dogadjaji.FindAsync(idDog);
        
            
        if(korisnik!=null){
            if(korisnik.Dogadjaji!=null  && dogadjajj!=null){
            korisnik.Dogadjaji.Remove(dogadjajj);
            await Context.SaveChangesAsync();
            return Ok($"uklonjen je dogadjaj od interesovanja {idDog} kod korisnika {email}");
            }
            else return Ok("ne postoji taj dogadjaj ili je lista dogadjaja prazna");
        }
        else
        {
            return BadRequest("ne postoji taj korisnik");
        }
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }
  
    [HttpGet("PreuzmiDogadjajeZaKojeJeKorisnikZainteresovan/{email}")]   //nema dogadjaaj za koje je zainteresovan uvek  vrati, 
    // jer je korisnik.Dogadjaj uvek null, a kad ukljucim Include(p=>p.Dogadjaji) baca error 500 u swaggeru
    public async Task<IActionResult> PreuzmiDogadjajeZaKojeJeKorisnikZainteresovan(string email)  
    {
        try{
        var korisnik=await Context.Korisnici.Include(x=>x.Dogadjaji).Where(p=>p.Email==email).FirstOrDefaultAsync();
        if(korisnik!=null)
        {
           
          //  return korisnik.Dogadjaji;//Ok(korisnik.Dogadjaji);
           //List<Dogadjaji> dogadjaji =  korisnik.Dogadjaji;
           if(korisnik.Dogadjaji!=null)
           {
           return Ok(korisnik.Dogadjaji.Select(r=> new
                                        {
                                            r.ID,
                                            r.Naziv,
                                            r.Mesto,
                                            r.CenaKarte,
                                            r.DatumVreme,
                                            r.DatumVreme.Day,
                                            r.DatumVreme.Month,
                                            r.DatumVreme.Year,
                                            r.DatumVreme.TimeOfDay.Hours,
                                            r.DatumVreme.TimeOfDay.Minutes
                                        }).ToList());
           }
           else return Ok("nema dogadjaja za koje je zainteresovan");                       
           
           
        }
        else{
            return Ok("ne postoji korisnik");
        }
        }catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
/*
    [HttpGet("PreuzmiDogadjajeOrganizatora/{email}")]   
    public async Task<IActionResult> PreuzmiDogadjajeOrganizatora(string email)  
    {
        try{
        var korisnik=await Context.Korisnici.Where(p=>p.Email==email).FirstOrDefaultAsync();
        if(korisnik!=null && (korisnik.TipKorisnika=="Organizator" ||korisnik.TipKorisnika=="organizator" || korisnik.TipKorisnika=="Administrator" || korisnik.TipKorisnika=="administrator"))
        {
            //ne umem da vratim raylicite tipove dogadjaja kroz jedno return OK
        }
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }*/

}
