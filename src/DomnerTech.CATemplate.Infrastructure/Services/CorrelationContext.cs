using DomnerTech.CATemplate.Application.Constants;
using Microsoft.AspNetCore.Http;
using Mobile.CleanArchProjectTemplate.Application.Services;

namespace DomnerTech.CATemplate.Infrastructure.Services;

public sealed class CorrelationContext(IHttpContextAccessor accessor) : ICorrelationContext
{
    public string CorrelationId =>
        accessor.HttpContext?.Items[HeaderConstants.CorrelationContextKey]?.ToString()
        ?? "N/A";
}