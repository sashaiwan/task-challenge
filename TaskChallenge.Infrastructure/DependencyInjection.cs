using Microsoft.Extensions.DependencyInjection;
namespace TaskChallenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TaskDbContext>(optionsLifetime: ServiceLifetime.Scoped);
                
        return services;
    }
}
