using Bas24.CommandQuery;
using DomnerTech.CATemplate.Application.DTOs;
using DomnerTech.CATemplate.Application.DTOs.Users;

namespace DomnerTech.CATemplate.Application.Features.Users.Handlers;

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponse<UserDto>>
{
    public async Task<BaseResponse<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResponse<UserDto>
        {
            Data = new UserDto
            {
                UserId = request.UserId,
                Username = "JohnDoe"
            }
        });
    }
}