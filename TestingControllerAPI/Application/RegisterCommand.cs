namespace TestingControllerAPI.Application
{
    public class RegisterCommand
    {
        public async Task<IResult> Register(UserRegisterModel user, IUserData data)
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
