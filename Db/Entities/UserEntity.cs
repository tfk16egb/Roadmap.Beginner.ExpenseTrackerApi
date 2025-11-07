namespace Roadmap.Beginner.ExpenseTrackerApi.Db.Models;

public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }


    public ICollection<ExpenseEntity> Expenses { get; set; } = new List<ExpenseEntity>();
}
