using Application.Services.UserServices;
using Application.User.UserVerify;
using Application.UserRegister;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly IUserRegisterCommand _command;
        private readonly IUserServices _services;
        private readonly IUserVerifyCommand _verifyCommand;
        public UserRegisterController(IUserRegisterCommand command, IUserServices services, IUserVerifyCommand verifyCommand)
        {
            _services = services;
            _command = command;
            _verifyCommand = verifyCommand;
        }

        [HttpPost("Register")]
        public async Task<UserRegisterDto> Register(UserRegisterModel request)
        {
            UserVerifyModel VerifyRequest = new UserVerifyModel()
            {
                Email = request.Email,
                Hash = Hash(),
                Code = GetOtp(),
            };
            var confirmationLink = Url.Action(nameof(UserVerify), "UserRegister", VerifyRequest, Request.Scheme);
            var results = await _command.Register(request, confirmationLink, VerifyRequest.Code, VerifyRequest.Hash);
            return results;

        }
        [HttpPost("Verify"), Authorize]
        public Task<UserVerifyDto> UserVerify(UserVerifyModel user)
        {
            int userId = int.Parse(_services.GetId());
            var result = _verifyCommand.UserVerify(user);
            return result;
        }

        private int GetOtp()
        {
            Random rnd = new Random();
            int code = rnd.Next(100000, 999999);
            return code;
        }

        private string Hash()
        {
            string hash = Guid.NewGuid().ToString("N").ToLower();
            return hash;
        }
    }
}
