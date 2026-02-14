using Bas24.CommandQuery;
using DomnerTech.CATemplate.Application.Abstractions;
using DomnerTech.CATemplate.Application.DTOs;
using DomnerTech.CATemplate.Application.DTOs.Users;

namespace DomnerTech.CATemplate.Application.Features.Users;

public sealed record GetUserQuery(Guid UserId) : IRequest<BaseResponse<UserDto>>, ILogCreator, IValidatableRequest;