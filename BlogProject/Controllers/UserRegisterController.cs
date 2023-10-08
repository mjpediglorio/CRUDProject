using DataAccess.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly IUserData data;
        public UserRegisterController(IUserData data)
        {
            this.data = data;
        }

        [HttpPost("Register")]
        public async Task<SignUpDto> Register(UserRegisterModel request)
        {

                var results = await data.RegisterUser(request);
            return results;

        }
    }
}
