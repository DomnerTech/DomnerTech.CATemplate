using Bas24.CommandQuery;
using DomnerTech.CATemplate.Application.DTOs;
using DomnerTech.CATemplate.Application.DTOs.Users;
using DomnerTech.CATemplate.Application.IRepo;
using Microsoft.Extensions.Caching.Distributed;

namespace DomnerTech.CATemplate.Application.Features.Users.Handlers;

public sealed class GetUserQueryHandler(IRedisCache redisCache) : IRequestHandler<GetUserQuery, BaseResponse<UserDto>>
{
    public async Task<BaseResponse<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $":users:{request.UserId}";
        var userCache = await redisCache.GetObjectAsync<UserDto>(cacheKey, cancellationToken);
        if (userCache != null)
            return new BaseResponse<UserDto>
            {
                Data = userCache
            };
        var user = new UserDto
        {
            UserId = request.UserId,
            Username = "JohnDoe"
        };
        await redisCache.SetObjectAsync(cacheKey, user, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(360)
        }, cancellationToken);
        return await Task.FromResult(new BaseResponse<UserDto>
        {
            Data = user
        });
    }
}