using Docado.BusinessLogic.Implementations.Authentication;
using Docado.BusinessLogic.Implementations.Users;
using Docado.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Docado.BusinessLogic.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDocadoServices(this IServiceCollection services) {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}