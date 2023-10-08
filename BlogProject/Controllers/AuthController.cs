using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserData data;

        public AuthController(IUserData data)
        {
            this.data = data;
        }
        [HttpPost("Login")]
        public async Task<AuthDto> Login(UserLoginModel request)
        {
            var results = await data.UserLogin(request);
            return results;
        }
    }
}
