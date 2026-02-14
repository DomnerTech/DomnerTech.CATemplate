using Bas24.CommandQuery;
using DomnerTech.CATemplate.Application.DTOs;
using DomnerTech.CATemplate.Application.DTOs.Users;
using DomnerTech.CATemplate.Application.Features.Users;
using Microsoft.AspNetCore.Mvc;

namespace DomnerTech.CATemplate.Api.Controllers;

public sealed class UserController(ICommandQuery commandQuery) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<BaseResponse<UserDto>>> GetUser([FromBody] GetUserReqDto r)
    {
        var user = await commandQuery.Send(new GetUserQuery(r.UserId), HttpContext.RequestAborted);
        return user.ReturnJson();
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponse<bool>>> CreateUser([FromBody] CreateUserDto r)
    {
        var result = await commandQuery.Send(new CreateUserCommand(r.UserId, r.Username, r.Age), HttpContext.RequestAborted);
        return result.ReturnJson();
    }
}