using DomnerTech.CATemplate.Application;
using DomnerTech.CATemplate.Application.IRepo;
using Elastic.Apm.StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DomnerTech.CATemplate.Infrastructure.Caching.Redis;

public static class Extensions
{
    public static IServiceCollection AddRedis(this IServiceCollection services, AppSettings appSettings)
    {
        var configOption = new ConfigurationOptions
        {
            Ssl = appSettings.Redis.Ssl
        };

        foreach (var redisConfigUrl in appSettings.Redis.EndPoints) configOption.EndPoints.Add(redisConfigUrl);
        configOption.User = appSettings.Redis.Username;
        configOption.Password = appSettings.Redis.Password;
        configOption.ConnectTimeout = appSettings.Redis.ConnectTimeout;

        var connection = ConnectionMultiplexer.Connect(configOption);
        connection.UseElasticApm();

        services.AddSingleton<IConnectionMultiplexer>(connection);
        services.AddStackExchangeRedisCache(config =>
            {
                config.ConnectionMultiplexerFactory = () => Task.FromResult<IConnectionMultiplexer>(connection);
            }
        );

        services.AddSingleton<IRedisCacheClient, RedisCache>();

        return services;
    }
}
