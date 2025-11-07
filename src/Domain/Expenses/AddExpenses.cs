using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.Db.Models;
using Roadmap.Beginner.ExpenseTrackerApi.src.Validators;
using System.Security.Claims;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Expenses;

public static class AddExpenses
{
    public static void MapAddExpensesEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/add", ([FromBody] AddExpenseCommand command, ClaimsPrincipal cp, ExpenseTrackerDbContext dbContext, IValidator<ClaimsPrincipal> validator) =>
        {
            var validationResult = validator.Validate(cp);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.ToDictionary().ToErrors("/add"));

            var userId = ((ClaimValidator)validator).GetUserId();

            var createdExpense = command.ToEntity(userId);

            dbContext.Expense.Add(createdExpense);
            dbContext.SaveChanges();

            return Results.Created($"/get/{createdExpense.Id}", createdExpense);

        });
    }
    public static ExpenseEntity ToEntity(this AddExpenseCommand command, int UserId) => new ExpenseEntity
    {
        UserId = UserId,
        Description = command.Description,
        Amount = command.Amaount,
        DateOfTransaction = DateTimeOffset.UtcNow,
        Currency = command.currency,
        ExpenseCategory = command.expenseCategory ?? ExpenseCategory.Other
    };
}
public record AddExpenseCommand(string Description, decimal Amaount, Currency currency, ExpenseCategory? expenseCategory);