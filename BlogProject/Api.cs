using DataAccess.Data;
using DataAccess.DbAccess;
using System.Security.Cryptography;

namespace BlogProject
{
    public static class Api
    {
        public static void ConfigureApi(this WebApplication app)
        {
            app.MapPost("/Register", Register);
            app.MapPost("/Login", Login);
        }

        private static async Task<IResult> Register(UserRegisterModel user, IUserData data)
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

        private static async Task<IResult> Login(UserLoginModel user, IUserData data)
        {
            try
            {
                var results = await data.UserLogin(user);
                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}