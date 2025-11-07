using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Auth;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Validators;

internal sealed class SignupValidator : AbstractValidator<SignupRequest>
{
    private readonly ExpenseTrackerDbContext dbContext;

    public SignupValidator(ExpenseTrackerDbContext dbContext)
    {
        this.dbContext = dbContext;

        //Username validation
        RuleFor(x => x.Username).SetValidator(new SignupUsernameValidator(this.dbContext));

        //Password validation
        RuleFor(x => x.Password).SetValidator(new SignupPasswordValidator(this.dbContext));

    }

}

internal sealed class SignupPasswordValidator : AbstractValidator<string>
{
    private readonly ExpenseTrackerDbContext dbContext;
    public SignupPasswordValidator(ExpenseTrackerDbContext dbContext)
    {
        this.dbContext = dbContext;

        RuleFor(password => password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

    }
}

public class SignupUsernameValidator : AbstractValidator<string>
{
    private readonly ExpenseTrackerDbContext dbContext;

    public SignupUsernameValidator(ExpenseTrackerDbContext dbContext)
    {
        this.dbContext = dbContext;

        RuleFor(username => username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters.")
            .MustAsync(BeUniqueUsername).WithMessage("Username already exists.");
    }


    private async Task<bool> BeUniqueUsername(string username, CancellationToken token)
        => !await dbContext.User.AnyAsync(u => u.Username == username);
}
