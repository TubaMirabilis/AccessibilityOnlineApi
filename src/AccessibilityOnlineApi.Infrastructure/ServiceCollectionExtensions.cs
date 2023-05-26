using AccessibilityOnlineApi.Application.Interfaces;
using AccessibilityOnlineApi.Application.Repositories;
using AccessibilityOnlineApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccessibilityOnlineApi.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["MariaDB:ConnectionString"];
        return services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        })
        .AddTransient<IUserRepository, UserRepository>()
        .AddTransient<IKeyRepository, KeyRepository>()
        .AddHttpClient()
        .AddScoped<IMailService, MailChimpService>();
    }
}