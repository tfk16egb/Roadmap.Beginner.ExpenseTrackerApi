using FluentValidation;

namespace Roadmap.Beginner.ExpenseTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddAuthorization();
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
            builder.Services.AddJwtConfiguration(builder.Configuration);
            builder.Services.AddDatabaseContext(builder.Configuration);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapGroup("/api/auth")
                .AddAuthEndpoints(builder.Configuration);

            app.MapGroup("/api/expenses")
                .RequireAuthorization()
                .AddExpenseEndpoints();



            app.Run();
        }
    }
}
