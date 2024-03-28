using FuDever.Application.Features.Auth.Login;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - user is locked out http response.
/// </summary>
internal sealed class UserIsLockedOutHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    public UserIsLockedOutHttpResponse(LoginRequest request)
    {
        HttpCode = StatusCodes.Status429TooManyRequests;
        AppCode = AuthAppCode.USER_IS_LOCKED_OUT;
        ErrorMessages =
        [
            $"User with username = {request.Username} has been temporarily locked due to entering the wrong password too many times",
            $"Please try again after 15 seconds."
        ];
    }
}
