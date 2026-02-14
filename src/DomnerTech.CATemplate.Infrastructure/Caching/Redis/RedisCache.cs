using System.Text.Json;
using DomnerTech.CATemplate.Application.IRepo;
using DomnerTech.CATemplate.Application.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace DomnerTech.CATemplate.Infrastructure.Caching.Redis;

public class RedisCache(
    IDistributedCache cache,
    ILogger<RedisCache> logger) : IRedisCacheClient
{
    public Task SetObjectAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        return SetObjectAsync(key, value, new DistributedCacheEntryOptions(), cancellationToken);
    }

    public async Task SetObjectAsync<T>(string key,
        T value,
        DistributedCacheEntryOptions options,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var json = JsonConvert.SerializeObject(value, DefaultJsonSerializerSettings.SnakeCase);
            await cache.SetStringAsync(key, json, options, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "redis: failed setting - {@Message}", e.Message);
        }
    }

    public async Task<T?> GetObjectAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var value = await cache.GetStringAsync(key, cancellationToken);

            return value is not null ? JsonConvert.DeserializeObject<T>(value, DefaultJsonSerializerSettings.SnakeCase) : default;
        }
        catch (Exception e)
        {
            logger.LogError(e, "redis: failed getting - {@Message}", e.Message);
        }

        return default;
    }

    public async Task SetObjectAsync<T>(string key,
        T value,
        DistributedCacheEntryOptions options,
        JsonSerializerOptions setting,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var json = JsonConvert.SerializeObject(value, setting);
            await cache.SetStringAsync(key, json, options, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "redis: failed setting - {@Message}", e.Message);
        }
    }

    public async Task<T?> GetObjectAsync<T>(string key, JsonSerializerOptions setting, CancellationToken cancellationToken = default)
    {
        try
        {
            var value = await cache.GetStringAsync(key, cancellationToken);

            return value is not null ? JsonConvert.DeserializeObject<T>(value, setting) : default;
        }
        catch (Exception e)
        {
            logger.LogError(e, "redis: failed getting - {@Message}", e.Message);
        }

        return default;
    }
}
