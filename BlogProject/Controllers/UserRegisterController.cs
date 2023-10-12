using Application.Processes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly IProcesses data;
        public UserRegisterController(IProcesses data)
        {
            this.data = data;
        }

        [HttpPost("Register")]
        public async Task<SignUpDto> Register(UserRegisterModel request)
        {

            var results = await data.Register(request);
            return results;

        }
    }
}
