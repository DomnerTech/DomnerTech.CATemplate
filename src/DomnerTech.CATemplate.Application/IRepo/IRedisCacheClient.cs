using Microsoft.Extensions.Caching.Distributed;

namespace DomnerTech.CATemplate.Application.IRepo;

public interface IRedisCacheClient : IBaseRepo
{
    Task SetObjectAsync<T>(string key, T value, CancellationToken cancellationToken = default);
    Task SetObjectAsync<T>(string key, T value, DistributedCacheEntryOptions options, CancellationToken cancellationToken = default);
    Task<T?> GetObjectAsync<T>(string key, CancellationToken cancellationToken = default);
}