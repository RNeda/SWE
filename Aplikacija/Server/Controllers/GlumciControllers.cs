using Microsoft.AspNetCore.Mvc;
using Models;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class GlumciController : ControllerBase
{
    public Context Context {get; set; }

    public GlumciController (Context context)
    {
        Context=context;
    }


    [HttpGet("VratiGlumca/{glumacID}")] //radi
    public async Task<ActionResult> VratiGlumcaNeda(int glumacID)
    {
        try
        {
            var glumac = Context.Glumci.FindAsync(glumacID);
            if (glumac != null)
            {
                //Context.Glumci.Remove(glumac);
                //await Context.SaveChangesAsync();
                return Ok(await glumac);
            }
            else
            {
                return BadRequest($"Nije pronađen glumac sa ID: {glumacID}");
            }
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

     [HttpPut("IzmeniGlumca/{glumacID}")]//radi
    public async Task<ActionResult> IzmeniGlumcaNeda([FromBody]Glumci glumac, int glumacID)
    {
        try
        {
            var stariGlumac = await Context.Glumci.FindAsync(glumacID);

            if (stariGlumac != null)
            {
                if(glumac.Ime!="string")
                {
                stariGlumac.Ime = glumac.Ime;
                }
                
                if(glumac.Prezume!="string")
                {
                stariGlumac.Prezume = glumac.Prezume;
                }
                Context.Glumci.Update(stariGlumac);
                await Context.SaveChangesAsync();
                return Ok($"ID izmenjenog glumca je: {glumacID}");
            }
            else
            {
                return BadRequest("Nije uspela izmana glumca!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PreuzmiGlumce")]
    public async Task<ActionResult> PreuzmiGlumce()
    {
        var podaci = (await Context
            .Glumci
            .ToListAsync());
        

        return Ok(podaci.
                            Select(r => new 
                            {
                                r.ID,
                                r.Ime,
                                r.Prezume
                            }).ToList());
    }

    [HttpPost("DodajGlumcaUFilm/{idFilma}")]
    public async Task<ActionResult> DodajGlumcaUFilm([FromBody]Glumci glumac,int idFilma)
    {
        var film=await Context.Filmovi.Include(p=>p.Glumci).Where(p=>p.ID==idFilma).FirstOrDefaultAsync();
      if(film!=null && film.Glumci!=null)
        {   film.Glumci.Add(glumac);
            await Context.Glumci.AddAsync(glumac);
            await Context.SaveChangesAsync();
            return Ok($"ID novog glumca je = {glumac.ID}");
        }
        else
        {
            return BadRequest("Neuspelo");
        }
    }
 [HttpPost("DodajGlumcaUPredstavu/{idPredstave}")]
    public async Task<ActionResult> DodajGlumcaUPredstavu([FromBody]Glumci glumac,int idPredstave)
    {
        var film=await Context.Predstave.Include(p=>p.Glumci).Where(p=>p.ID==idPredstave).FirstOrDefaultAsync();
      if(film!=null)
        {   film.Glumci.Add(glumac);
            await Context.Glumci.AddAsync(glumac);
            await Context.SaveChangesAsync();
            return Ok($"ID novog glumca je = {glumac.ID}");
        }
        else
        {
            return BadRequest("Neuspelo");
        }
    }

    [HttpPut("IzmeniGlumca/{id}")]
    public async Task<ActionResult> IzmeniGlumca(int id, [FromBody]Glumci glumac)
    {
        var stara = await Context.Glumci.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(glumac.Ime) &&
            !string.IsNullOrWhiteSpace(glumac.Prezume))
        {
            stara.Ime = glumac.Ime;
            stara.Prezume= glumac.Prezume;

            Context.Glumci.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjen glumac ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

    [HttpDelete("IzbrisiGlumca/{id}")]
    public async Task<ActionResult> IzbrisatiGlumca(int id)
    {
        var stara = await Context.Glumci.FindAsync(id);

        if (stara != null)
        {

            Context.Glumci.Remove(stara);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan glumac sa ID={id}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }
}