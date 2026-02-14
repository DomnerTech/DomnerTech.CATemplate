using DomnerTech.CATemplate.Application.Errors;
using Microsoft.AspNetCore.Http;

namespace DomnerTech.CATemplate.Application.Exceptions;

public sealed class UserIdRequiredException() : BaseErrorException("UserId is required")
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Code => ErrorCodes.HeaderMissing;
}