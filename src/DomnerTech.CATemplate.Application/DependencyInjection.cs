using System.Reflection;
using Bas24.CommandQuery;
using DomnerTech.CATemplate.Application.Pipelines;
using Elastic.Apm.AspNetCore.DiagnosticListener;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.Instrumentations.SqlClient;
using Elastic.Apm.MongoDb;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mobile.CleanArchProjectTemplate.Application;

namespace DomnerTech.CATemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCommandQuery(cfg =>
        {
            cfg.RegisterAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(LoggingPipelineBehavior<,>));
            cfg.AddBehavior(typeof(ValidationBehavior<,>));
        });

        var appAssembly = typeof(ApplicationAssemblyMarker).Assembly;

        services.AddValidatorsFromAssembly(appAssembly);

        ValidationStartupChecks.EnsureAllValidatorsRegistered(
            services,
            appAssembly
        );

        services.AddElasticApm(
            new HttpDiagnosticsSubscriber(),
            new AspNetCoreDiagnosticSubscriber(),
            new SqlClientDiagnosticSubscriber(),
            new MongoDbDiagnosticsSubscriber());
        return services;
    }
}