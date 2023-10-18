using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")] //https://localhost5001/api/users - UserController, the [controller] gets rid of the Controller at the end of out class
    public class BaseApiController : ControllerBase
    {

    }
}
