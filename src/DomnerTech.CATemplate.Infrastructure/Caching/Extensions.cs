using DomnerTech.CATemplate.Application;
using DomnerTech.CATemplate.Infrastructure.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace DomnerTech.CATemplate.Infrastructure.Caching;
public static class Extensions
{
    public static IServiceCollection AddCache(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddRedis(appSettings);
        return services;
    }
}
