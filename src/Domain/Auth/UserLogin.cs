using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.Db.Models;
using Roadmap.Beginner.ExpenseTrackerApi.src.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Auth;



public static class UserLogin
{
    public static void MapUserLogin(this RouteGroupBuilder group, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");

        group.MapPost("/login", async (LoginRequest login, ExpenseTrackerDbContext dbContext, IValidator<LoginRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(login);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.ToDictionary().ToErrors("/login"));

            var userEntity = ((LoginValidator)validator).GetUser();

            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;

            JwtTokenHelper(login, jwtSettings, userEntity, out tokenHandler, out token);

            return Results.Ok(new { token = tokenHandler.WriteToken(token) });
        });

    }

    private static void JwtTokenHelper(
           LoginRequest login,
           IConfigurationSection jwtSettings,
           UserEntity userEntity, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
    {
        tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub,login.Username),
                    new Claim("userId", userEntity.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = signingCredentials,
        };

        token = tokenHandler.CreateToken(tokenDescriptor);
    }
}

public record LoginRequest(string Username, string Password);
