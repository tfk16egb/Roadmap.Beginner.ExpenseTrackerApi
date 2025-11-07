using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Roadmap.Beginner.ExpenseTrackerApi.Db;
using Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Auth;
using Roadmap.Beginner.ExpenseTrackerApi.src.Domain.Expenses;
using System.Text;

namespace Roadmap.Beginner.ExpenseTrackerApi;

public static class Bootstrapper
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection sc, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

        sc.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

        return sc;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection sc, IConfiguration configuration)
    {
        sc.AddDbContext<ExpenseTrackerDbContext>((sp, opt) =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));
        });

        return sc;
    }

    public static void AddAuthEndpoints(this RouteGroupBuilder authGroup, IConfiguration configuration)
    {
        authGroup.MapUserLogin(configuration);
        authGroup.MapUserSignup();
    }
    public static void AddExpenseEndpoints(this RouteGroupBuilder expenseGroup)
    {
        expenseGroup.MapGetExpensesEndpoint();
        expenseGroup.MapAddExpensesEndpoint();
        expenseGroup.MapUpdateExpensesEndpoint();
        expenseGroup.MapRemoveExpensesEndpoint();
    }
}
