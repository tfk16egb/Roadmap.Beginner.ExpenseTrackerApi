namespace Roadmap.Beginner.ExpenseTrackerApi.Db.Models;

public class ExpenseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset DateOfTransaction { get; set; }
    public Currency Currency { get; set; } = Currency.SEK;
    public ExpenseCategory ExpenseCategory { get; set; } = ExpenseCategory.Other;
}


public enum ExpenseCategory
{
    Groceries,
    Leisure,
    Electronics,
    Clothing,
    Food,
    Transportation,
    Entertainment,
    Utilities,
    Healthcare,
    Other
}

public enum Currency
{
    SEK,
    USD,
    EUR,
    GBP,
    INR,
    JPY
}
