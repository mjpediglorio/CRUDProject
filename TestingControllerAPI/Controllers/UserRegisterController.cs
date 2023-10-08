using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestingControllerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly RegisterCommand _register;

        public UserRegisterController(RegisterCommand register)
        {
            _register = register;
        }

    }
    [HttpPost("Register")]
    public async Task<IResult> Register(UserRegisterModel user)
    {
        try
        {
            await data.RegisterUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {

            return Results.Problem(ex.Message);
        }
    }
}
}
