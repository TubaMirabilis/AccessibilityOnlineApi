using AccessibilityOnlineApi.Application.Dtos;
using AccessibilityOnlineApi.Application.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AccessibilityOnlineApi.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddScoped<IValidator<CreateUserDTO>, CreateUserValidator>()
            .AddScoped<UserRegistrationHandler>();
    }
}