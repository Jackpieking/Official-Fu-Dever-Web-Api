using FuDever.Application.Features.Auth.Login;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - user is not found http response.
/// </summary>
internal sealed class UserIsNotFoundHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    public UserIsNotFoundHttpResponse(LoginRequest request)
    {
        HttpCode = StatusCodes.Status404NotFound;
        AppCode = AuthAppCode.USER_IS_NOT_FOUND;
        ErrorMessages =
        [
            $"User with username = {request.Username} is not found."
        ];
    }
}
