using Application.Models;
using Application.Processes;
using Application.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IProcesses _process;
        private readonly IUserServices _services;
        public UserController(IProcesses process, IUserServices services)
        {
            _services = services;
            _process = process;
        }
        [HttpGet("Get"), Authorize]
        public Task<AccountInfoDto> AccountUserInfoGet()
        {
            int userId =  int.Parse(_services.GetId());
            var result = _process.GetUserInfo(userId);
            return result;
        }
    }
}
