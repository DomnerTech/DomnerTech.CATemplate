using DomnerTech.CATemplate.Application;
using DomnerTech.CATemplate.Infrastructure.Caching;
using DomnerTech.CATemplate.Infrastructure.Repo;
using DomnerTech.CATemplate.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Mobile.CleanArchProjectTemplate.Application.Services;

namespace DomnerTech.CATemplate.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddCache(appSettings)
            .AddRepo()
            .AddServices();

        services.AddHttpContextAccessor();
        services.AddScoped<ICorrelationContext, CorrelationContext>();
    }
}