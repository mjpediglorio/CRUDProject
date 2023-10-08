using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly IUserData data;
        [HttpPost("Register")]
        public async Task<IResult> RegisterUser(UserRegisterModel user)
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
