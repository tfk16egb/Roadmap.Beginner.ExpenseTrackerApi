using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.Db.Models;
using Roadmap.Beginner.ExpenseTrackerApi.src.Validators;
using System.Security.Claims;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Expenses;

public static class UpdateExpenses
{
    public static void MapUpdateExpensesEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/update", async ([FromBody] UpdateExpenseCommand command, ClaimsPrincipal cp, ExpenseTrackerDbContext dbContext, IValidator<ClaimsPrincipal> validator) =>
        {
            var validationResult = validator.Validate(cp);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.ToDictionary().ToErrors("/update"));

            var userId = ((ClaimValidator)validator).GetUserId();

            var expense = await dbContext.Expense.FirstOrDefaultAsync(e => e.Id == command.Id && e.UserId == userId);

            if (expense == null)
                return Results.NotFound("Expense not found");

            expense.UpdateFrom(command);

            await dbContext.SaveChangesAsync();
            return Results.Ok(expense);
        });
    }
    internal static void UpdateFrom(this ExpenseEntity expense, UpdateExpenseCommand command)
    {
        if (command.Description is not null)
            expense.Description = command.Description;

        if (command.Amount is not null)
            expense.Amount = command.Amount.Value;

        if (command.Currency is not null)
            expense.Currency = command.Currency.Value;

        if (command.ExpenseCategory is not null)
            expense.ExpenseCategory = command.ExpenseCategory.Value;
    }
}
public record UpdateExpenseCommand(int Id, string Description, decimal? Amount, Currency? Currency, ExpenseCategory? ExpenseCategory);