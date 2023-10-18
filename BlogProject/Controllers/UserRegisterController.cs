using Application.Processes;
using Application.UserRegister;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly UserRegisterCommand _command;
        public UserRegisterController(UserRegisterCommand command)
        {
            _command = command;
        }

        [HttpPost("Register")]
        public async Task<UserRegisterDto> Register(UserRegisterModel request)
        {

            var results = await _command.Register(request);
            return results;

        }
    }
}
