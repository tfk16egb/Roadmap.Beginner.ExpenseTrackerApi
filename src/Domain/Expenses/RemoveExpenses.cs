using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.src.Validators;
using System.Security.Claims;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Expenses;

public static class RemoveExpenses
{
    public static void MapRemoveExpensesEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/remove", async (
            DeleteExpenseCommand command,
            ClaimsPrincipal cp,
            ExpenseTrackerDbContext dbContext,
            IValidator<ClaimsPrincipal> validator) =>
        {
            var validationResult = validator.Validate(cp);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.ToDictionary().ToErrors("/remove"));

            var userId = ((ClaimValidator)validator).GetUserId();

            var expense = await dbContext.Expense.FirstOrDefaultAsync(e => e.Id == command.id && e.UserId == userId);

            if (expense == null)
                return Results.NotFound("Expense not found");

            dbContext.Expense.Remove(expense);
            await dbContext.SaveChangesAsync();

            return Results.Ok($"Expense removed: Id[{command.id}]");
        });
    }
}
public record DeleteExpenseCommand(int id);

