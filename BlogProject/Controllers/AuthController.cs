using Application.Processes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IProcesses data;

        //public AuthController(IProcesses data)
        //{
        //    this.data = data;
        //}
        //[HttpPost("Login")]
        //public async Task<AuthDto> Login(UserLoginModel request)
        //{
        //    var results = await data.Login(request);
        //    return results;
        //}
    }
}
