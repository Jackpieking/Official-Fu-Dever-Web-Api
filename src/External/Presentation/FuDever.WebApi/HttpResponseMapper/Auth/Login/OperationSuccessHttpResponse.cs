using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;
using FuDever.Application.Features.Auth.Login;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    internal OperationSuccessHttpResponse(LoginResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.ResponseBody;
    }
}
