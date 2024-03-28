using FuDever.Application.Features.Auth.Login;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - user email is not confirmed
///     http response.
/// </summary>
internal sealed class UserEmailIsNotConfirmedHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    public UserEmailIsNotConfirmedHttpResponse(LoginRequest request)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = AuthAppCode.USER_EMAIL_IS_NOT_CONFIRMED;
        ErrorMessages =
        [
            $"User with username = {request.Username} has not confirmed account creation email."
        ];
    }
}
