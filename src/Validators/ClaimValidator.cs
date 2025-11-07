using FluentValidation;
using System.Security.Claims;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Validators;

public class ClaimValidator : AbstractValidator<ClaimsPrincipal>
{
    private int _userId;

    public ClaimValidator()
    {
        RuleFor(claim => claim)
            .Must(HaveValidUserIdClaim).WithMessage("Invalid or missing userId claim.");

    }

    public int GetUserId() => _userId;


    private bool HaveValidUserIdClaim(ClaimsPrincipal cp)
    {
        var claim = cp.Claims.FirstOrDefault(c => c.Type == "userId");

        if (claim != null && int.TryParse(claim.Value, out var val))
        {
            _userId = val;
            return true;
        }

        return false;
    }
}
