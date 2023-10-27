using Application.Services.UserServices;
using Application.User.UserInfoGet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.User.UserDelete;
using Application.User.UserInfoUpdate;
using Application.User.UserVerify;
using Domain.Interfaces.Auth;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;
        private readonly IUserInfoGetCommand _userinfo;
        private readonly IUserDelete _userdelete;
        private readonly IUserInfoUpdateCommand _userinfoupdate;
        private readonly IUserVerifyCommand _userverify;
        private readonly IDbUser _db;
        public UserController(IUserInfoGetCommand userinfo, IUserServices services, IUserDelete userdelete, IUserInfoUpdateCommand userinfoupdate, IUserVerifyCommand userverify)
        {
            _userverify = userverify;
            _services = services;
            _userinfo = userinfo;
            _userdelete = userdelete;
            _userinfoupdate = userinfoupdate;
        }
        [HttpGet("Get"), Authorize]
        public Task<UserInfoGetDto> UserInfoGet()
        {
            int userId = int.Parse(_services.GetId());
            var result = _userinfo.UserInfoGet(userId);
            return result;
        }
        [HttpPatch("Update"), Authorize]

        public Task<UserInfoUpdateDto> UserInfoUpdate(UserInfoUpdateModel user)
        {
            int userId = int.Parse(_services.GetId());
            var result = _userinfoupdate.InfoUpdate(userId, user);
            return result;
        }

        [HttpDelete("Delete"), Authorize]
        public Task<UserDeleteDto> UserDelete(UserDeleteModel user)
        {
            int userId = int.Parse(_services.GetId());
            var result = _userdelete.UserDelete(userId, user);
            return result;
        }
    }
}
