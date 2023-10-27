using BlogProject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Application.Services.UserServices;
using Application.UserRegister;
using Domain.Interfaces.Auth;
using Application.UserSignIn;
using Application.User.UserInfoGet;
using Application.User.UserDelete;
using Application.User.UserInfoUpdate;
using Infrastructure.DbApp;
using Application.User.UserVerify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IUserRegisterCommand, UserRegisterCommand>();
builder.Services.AddScoped<IUserSignInCommand, UserSignInCommand>();
builder.Services.AddScoped<IUserInfoGetCommand, UserInfoGetCommand>();
builder.Services.AddScoped<IUserDelete, UserDeleteCommand>();
builder.Services.AddScoped<IUserInfoUpdateCommand, UserInfoUpdateCommand>();
builder.Services.AddScoped<IDbUser, DbUser>();
builder.Services.AddScoped<IUserVerifyCommand, UserVerifyCommand>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    { Title = "API Auth", Version = "1.0.1", Description = "API Auth" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddCors();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value))
        };
    });
Environment.SetEnvironmentVariable("IsTesting", "true");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
Console.WriteLine(Environment.GetEnvironmentVariable("IsTesting"));
app.UseCors("NgOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();