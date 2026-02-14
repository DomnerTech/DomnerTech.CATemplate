using Bas24.CommandQuery;
using DomnerTech.CATemplate.Application.Abstractions;
using DomnerTech.CATemplate.Application.DTOs;

namespace DomnerTech.CATemplate.Application.Features.Users;

public sealed record CreateUserCommand(Guid UserId, string Username, int Age) :
    IRequest<BaseResponse<bool>>,
    ILogCreator, 
    IValidatableRequest;