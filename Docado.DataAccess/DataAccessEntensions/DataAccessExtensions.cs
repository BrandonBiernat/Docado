using Docado.DataAccess.DB;
using Docado.DataAccess.Implementations.Users;
using Docado.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Docado.DataAccess.DataAccessEntensions;

public static class DataAccessExtensions {
    /// <summary>
    /// Adds configurations for exposing Data Access Layer 
    /// </summary>
    public static IServiceCollection AddDocadoDataAccess (
        this IServiceCollection services,
        string connectionString) {
        services.AddDbContext<DocadoDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
    
    /// <summary>
    /// Configures Identity for Docado
    /// </summary>
    public static IServiceCollection AddDocadoIdentity(
        this IServiceCollection services) {
        services.AddIdentityCore<UserRecord>()
            .AddEntityFrameworkStores<DocadoDbContext>();
        services.Configure<IdentityOptions>(options => {
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    
            options.SignIn.RequireConfirmedEmail = true;

            options.Password.RequiredLength = 12;
    
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        });
        services.Configure<PasswordHasherOptions>(option => {
            option.IterationCount = 12000;
        });
        
        return services;
    }
}