using DomnerTech.CATemplate.Application.IRepo;
using Microsoft.Extensions.DependencyInjection;

namespace DomnerTech.CATemplate.Infrastructure.Repo;

public static class Extensions
{
    public static IServiceCollection AddRepo(this IServiceCollection services)
    {
        services.Scan(s =>
            s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo<IBaseRepo>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
        return services;
    }
}