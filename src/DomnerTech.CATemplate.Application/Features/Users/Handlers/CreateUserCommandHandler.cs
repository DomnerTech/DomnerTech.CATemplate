using Bas24.CommandQuery;
using DomnerTech.CATemplate.Application.DTOs;

namespace DomnerTech.CATemplate.Application.Features.Users.Handlers;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
{
    public async Task<BaseResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResponse<bool>
        {
            Data = true
        });
    }
}