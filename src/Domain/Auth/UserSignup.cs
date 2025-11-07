using FluentValidation;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.Db.Models;
using Roadmap.Beginner.ExpenseTrackerApi.src.Validators;
using System.Security.Cryptography;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Auth;

public static class UserSignup
{
    public static void MapUserSignup(this RouteGroupBuilder group)
    {
        group.MapPost("/signup", async (SignupRequest signup, ExpenseTrackerDbContext dbContext, IValidator<SignupRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(signup);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.ToDictionary().ToErrors("/signup"));

            var user = new UserEntity
            {
                Username = signup.Username,
                Password = HashPassword(signup.Password)
            };

            dbContext.User.Add(user);
            await dbContext.SaveChangesAsync();
            return Results.Ok(new { user.Id, user.Username });
        });
    }

    private static string HashPassword(string password)
    {
        const int iterations = 100_000;
        const int saltSize = 16;
        const int keySize = 32;

        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[saltSize];
        rng.GetBytes(salt);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var key = pbkdf2.GetBytes(keySize);

        // store as: iterations.saltBase64.keyBase64
        return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
    }
}

public record SignupRequest(string Username, string Password);