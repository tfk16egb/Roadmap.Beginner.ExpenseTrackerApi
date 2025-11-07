using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.Db.Models;
using Roadmap.Beginner.ExpenseTrackerApi.src.Validators;
using System.Security.Claims;

namespace Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Expenses;

public static class GetExpenses
{
    public static void MapGetExpensesEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/get", async ([AsParameters] GetExpenseQuery query, ClaimsPrincipal cp, ExpenseTrackerDbContext dbContext, IValidator<ClaimsPrincipal> validator)
            =>
        {
            var validationResult = validator.Validate(cp);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.ToDictionary().ToErrors("/get"));

            var userId = ((ClaimValidator)validator).GetUserId();

            var expenses = await dbContext.Expense
                .Where(e => e.UserId == userId)
                .WhereDateInRange(e => e.DateOfTransaction, query.FromDate, query.ToDate)
                .Select(e => e.ToDto())
                .ToListAsync();

            return Results.Ok(expenses);

        });
    }
    public static ExpenseDto ToDto(this ExpenseEntity expenseEntity)
        => new ExpenseDto
        {
            Id = expenseEntity.Id,
            Description = expenseEntity.Description,
            Amount = expenseEntity.Amount,
            DateOfTransaction = expenseEntity.DateOfTransaction,
            currency = expenseEntity.Currency,
            expenseCategory = expenseEntity.ExpenseCategory
        };
}

public record GetExpenseQuery(DateTimeOffset? FromDate, DateTimeOffset? ToDate);


public class ExpenseDto
{
    public int Id { get; internal set; }
    public string Description { get; internal set; }
    public decimal Amount { get; internal set; }
    public DateTimeOffset DateOfTransaction { get; internal set; }
    public Currency currency { get; internal set; }
    public ExpenseCategory expenseCategory { get; internal set; }
}
