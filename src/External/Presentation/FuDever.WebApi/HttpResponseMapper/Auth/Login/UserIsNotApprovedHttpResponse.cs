using FuDever.Application.Features.Auth.Login;
using FuDever.WebApi.ApiReturnCodes;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - user is not approved http
///     response.
/// </summary>
internal sealed class UserIsNotApprovedHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    public UserIsNotApprovedHttpResponse(LoginRequest request)
    {
        HttpCode = StatusCodes.Status403Forbidden;
        AppCode = AuthAppCode.USER_IS_NOT_APPROVED;
        ErrorMessages =
        [
            $"User with username = {request.Username} is not approved by admin."
        ];
    }
}
