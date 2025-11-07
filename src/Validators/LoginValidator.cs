using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.Db.Models;
using Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Auth;
using System.Security.Cryptography;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Validators;

internal sealed class LoginValidator : AbstractValidator<LoginRequest>
{
    private readonly ExpenseTrackerDbContext dbContext;
    private UserEntity user;

    public LoginValidator(ExpenseTrackerDbContext dbContext)
    {
        this.dbContext = dbContext;

        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop)
            .MustAsync(BeExisting).WithMessage("User not found")
            .DependentRules(() =>
            {
                RuleFor(x => x.Password)
                    .Must(BeVerifiedPassword).WithMessage("Invalid password");
            });


    }

    public UserEntity GetUser() => user;

    private bool BeVerifiedPassword(string password)
    {
        if (string.IsNullOrEmpty(user.Password)) return false;

        var storedHash = user.Password;
        try
        {
            var parts = storedHash.Split('.', 3);
            if (parts.Length != 3) return false;

            var iterations = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var computedKey = pbkdf2.GetBytes(key.Length);

            return CryptographicOperations.FixedTimeEquals(computedKey, key);
        } catch
        {
            return false;
        }

    }

    private async Task<bool> BeExisting(string username, CancellationToken token)
    {
        user = await dbContext.User.FirstOrDefaultAsync(u => u.Username == username);
        return user != null;
    }
}
