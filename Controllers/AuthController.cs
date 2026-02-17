using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers       
{
    [Route("api/[controller]")]    // configuration= gets data from the appsettings.json file and uses it to determine the route for this controller. The [controller] placeholder will be replaced with the name of the controller, which in this case is "Auth". So, the route for this controller will be "api/auth".
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
    }
}
