using Correlate.AspNetCore;
using Correlate.DependencyInjection;
using DomnerTech.CATemplate.Api;
using DomnerTech.CATemplate.Api.ApiDocs;
using DomnerTech.CATemplate.Api.Auth;
using DomnerTech.CATemplate.Api.Middleware;
using DomnerTech.CATemplate.Application;
using System.Reflection;
using DomnerTech.CATemplate.Application.Constants;
using DomnerTech.CATemplate.Infrastructure;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("serilog.json", true, true);
    builder.Logging.ClearProviders();
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
    builder.Host.UseSerilog();
    builder.Services.AddControllerJsonSerializerOptions();
    var appSettings = new AppSettings();
    builder.Configuration.GetRequiredSection("AppSettings").Bind(appSettings);
    builder.Services.AddSingleton(appSettings);
    builder.Services.AddAuth(appSettings);
    builder.Services.AddSwaggerDoc(appSettings, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    builder.Services.AddCorrelate(options =>
    {
        options.RequestHeaders =
        [
            HeaderConstants.CorrelationId
        ];
    });

    builder.Services.AddHealthChecks();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services
        .AddApplication()
        .AddInfrastructure(appSettings);

    // Middleware
    builder.Services.AddTransient<RequestTimingMiddleware>();
    builder.Services.AddTransient<ErrorHandlingMiddleware>();
    builder.Services.AddTransient<CorrelationIdMiddleware>();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseSwaggerDoc(appSettings);
    app.UseCorrelate();
    app.UseMiddleware<CorrelationIdMiddleware>();

    app.UseSerilogRequestLogging();
    app.UseMiddleware<RequestTimingMiddleware>();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapHealthChecks("/healthz");
    app.MapControllers();
    await app.RunAsync();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}