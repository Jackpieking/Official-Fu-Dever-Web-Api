using FuDever.Application.Features.Auth.Login;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - user password is not correct
///     http response.
/// </summary>
internal sealed class UserPasswordIsNotCorrectHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    public UserPasswordIsNotCorrectHttpResponse(LoginRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = AuthAppCode.USER_PASSWORD_IS_NOT_CORRECT;
        ErrorMessages =
        [
            "Password is not valid."
        ];
    }
}
