using FuDever.Application.Features.Auth.Login;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - user is temporarily removed
///     http response.
/// </summary>
internal sealed class UserIsTemporarilyRemovedHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    public UserIsTemporarilyRemovedHttpResponse(LoginRequest request)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = AuthAppCode.USER_IS_TEMPORARILY_REMOVED;
        ErrorMessages =
        [
            $"User with username = {request.Username} has already been banned or blocked by Admin.",
            $"Please contact with admin to recover your account."
        ];
    }
}
