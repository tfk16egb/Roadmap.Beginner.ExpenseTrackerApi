using Microsoft.EntityFrameworkCore;
using Roadmap.Beginner.ExpenseTrackerApi.Db.Models;

namespace Roadmap.Beginner.ExpenseTrackerApi.Db;

public class ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> User { get; set; }

    public DbSet<ExpenseEntity> Expense { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.Password).IsRequired();
        });

        modelBuilder.Entity<ExpenseEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Amount).IsRequired();
            entity.Property(e => e.DateOfTransaction).IsRequired();
            entity.HasOne<UserEntity>()
                  .WithMany(u => u.Expenses)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ExpenseEntity>().HasData(
            new ExpenseEntity
            {
                Id = 1,
                UserId = 3,
                Description = "Sample Expense",
                Amount = 999.00m,
                DateOfTransaction = new DateTime(2024, 01, 01)
            },
            new ExpenseEntity
            {
                Id = 2,
                UserId = 2,
                Description = "Beer",
                Amount = 100.00m,
                DateOfTransaction = new DateTime(2025, 10, 9)
            },
            new ExpenseEntity
            {
                Id = 3,
                UserId = 2,
                Description = "Coffee",
                Amount = 200.00m,
                DateOfTransaction = new DateTime(2025, 10, 10)
            }
        );
    }
}
