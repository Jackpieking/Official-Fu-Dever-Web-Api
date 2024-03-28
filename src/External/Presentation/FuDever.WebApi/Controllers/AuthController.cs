using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using FuDever.Application.Features.Auth.Login;
using FuDever.WebApi.DTOs.Auth.Incomings;
using FuDever.WebApi.HttpResponseMapper.Others;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FuDever.WebApi.Controllers;

[Consumes(contentType: MediaTypeNames.Application.Json)]
[Produces(contentType: MediaTypeNames.Application.Json)]
[ApiController]
[Route(template: "api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;
    private readonly HttpResponseMapperManager _httpResponseMapperManager;

    public AuthController(
        ISender sender,
        HttpResponseMapperManager httpResponseMapperManager)
    {
        _sender = sender;
        _httpResponseMapperManager = httpResponseMapperManager;
    }

    /// <summary>
    ///     Endpoint for user login.
    /// </summary>
    /// <param name="dto">
    ///     Class contains user credentials.
    /// </param>
    /// <param name="cancellationToken">
    ///     Automatic initialized token for aborting current operation.
    /// </param>
    /// <returns>
    ///     A class contains access token, refresh token and
    ///     some user credentials.
    /// </returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Auth/login
    ///     {
    ///         "email": "user@example.com",
    ///         "password": "string",
    ///         "rememberMe": true
    ///     }
    ///
    /// </remarks>
    /// <response code="400"></response>
    /// <response code="404"></response>
    /// <response code="403"></response>
    /// <response code="200"></response>
    [HttpPost(template: "login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginUserDto dto,
        CancellationToken cancellationToken)
    {
        // Get all departments which are not temporarily removed.
        LoginRequest featureRequest = new()
        {
            CacheExpiredTime = 60,
            Username = dto.Username,
            Password = dto.Password,
            RememberMe = dto.RememberMe
        };

        var featureResponse = await _sender.Send(
            request: featureRequest,
            cancellationToken: cancellationToken);

        var apiResponse = _httpResponseMapperManager.Auth.Login
            .Resolve(statusCode: featureResponse.StatusCode)
            .Invoke(arg1: featureRequest, arg2: featureResponse);

        return StatusCode(
            statusCode: apiResponse.HttpCode,
            value: apiResponse);
    }
}
