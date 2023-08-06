using Microsoft.AspNetCore.Mvc;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class GostiController : ControllerBase
{
    public Context Context {get; set; }

    public GostiController (Context context)
    {
        Context=context;
    }

    //metodeeeee
    [HttpPost("UpisiGosta")]
    public async Task<ActionResult> UpisiGosta([FromBody]Gosti gost)
    {
        try
        {
        var gostt=new Gosti();
        gostt.Ime=gost.Ime;
        gostt.Prezume=gost.Prezume;
       
            await Context.Gosti.AddAsync(gostt);
            await Context.SaveChangesAsync();
            return Ok($"ID novog gosta je = {gost.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("IzmeniGosta/{id}")]
    public async Task<ActionResult> IzmeniGosta(int id, [FromBody]Gosti gost)
    {
        var stara = await Context.Gosti.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(gost.Ime) &&
            !string.IsNullOrWhiteSpace(gost.Prezume))
        {
            stara.Ime = gost.Ime;
            stara.Prezume= gost.Prezume;

            Context.Gosti.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjen gost ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }


    [HttpPut("IzmeniGostaNeda/{gostID}")]//radi
    public async Task<ActionResult> IzmeniGostaNeda([FromBody]Gosti gost, int gostID)
    {
        try
        {
            var stariGost = await Context.Gosti.FindAsync(gostID);

            if (stariGost != null)
            {
                if(gost.Ime!="string")
                {
                    stariGost.Ime = gost.Ime;
                }
                
                if(gost.Prezume!="string")
                {
                    stariGost.Prezume = gost.Prezume;
                }
                Context.Gosti.Update(stariGost);
                await Context.SaveChangesAsync();
                return Ok($"ID izmenjenog gosta je: {gostID}");
            }
            else
            {
                return BadRequest("Nije uspela izmana gosta!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


   [HttpDelete("IzbrisiGosta/{id}")]
    public async Task<ActionResult> IzbrisatiGosta(int id)
    {
        var stara = await Context.Gosti.FindAsync(id);

        if (stara != null)
        {

            Context.Gosti.Remove(stara);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisan gost sa ID={id}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

    [HttpGet("VratiGosta/{gostID}")]//radi
    public async Task<ActionResult> VratiGosta(int gostID)
    {
        try
        {
            var gost = Context.Gosti.FindAsync(gostID);
            if (gost != null)
            {
                //Context.Glumci.Remove(glumac);
                //await Context.SaveChangesAsync();
                return Ok(await gost);
            }
            else
            {
                return BadRequest($"Nije pronađen gost sa ID: {gostID}");
            }
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

   
    [HttpGet("PreuzmiGoste")]  //radi
    public async Task<ActionResult> PreuzmiGoste() 
    {
        var podaci = (await Context
            .Gosti
            .ToListAsync());
        

        return Ok(podaci.
                            Select(r => new 
                            {
                                r.ID,
                                r.Ime,
                                r.Prezume
                            }).ToList());
    }

}