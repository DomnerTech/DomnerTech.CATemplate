using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomnerTech.CATemplate.Api.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class BaseApiController : ControllerBase;