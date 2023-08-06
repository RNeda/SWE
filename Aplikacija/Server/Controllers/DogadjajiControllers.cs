using Microsoft.AspNetCore.Mvc;
using Models;

namespace Moj_Grad___project.Controllers;

[ApiController]
[Route("[controller]")]
public class DogadjajiController : ControllerBase
{
    public Context Context {get; set; }

    public DogadjajiController (Context context)
    {
        Context=context;
    }
}