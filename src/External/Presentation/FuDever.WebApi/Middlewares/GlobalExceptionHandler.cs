using FuDever.WebApi.ApiReturnCodes.Base;
using FuDever.WebApi.Commons;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.WebApi.Middlewares;

/// <summary>
///     Represent global exception handler for all error.
/// </summary>
internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.Clear();
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(
            value: new CommonResponse
            {
                ApiReturnCode = BaseApiReturnCode.SERVER_ERROR,
                ErrorMessages =
                [
                    "Server error.",
                    "Please try again later."
                ]
            },
            cancellationToken: cancellationToken);

        await httpContext.Response.CompleteAsync();

        return true;
    }
}
