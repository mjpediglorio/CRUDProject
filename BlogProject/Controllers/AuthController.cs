using Application.UserSignIn;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserSignInCommand _command;
        public AuthController(IUserSignInCommand command)
        {
            _command = command;
        }

        [HttpPost("Login")]
        public async Task<SignInDto> Login(UserSignInModel user)
        {
            var results = await _command.SignInCommand(user);
            return results;
        }
    }
}
