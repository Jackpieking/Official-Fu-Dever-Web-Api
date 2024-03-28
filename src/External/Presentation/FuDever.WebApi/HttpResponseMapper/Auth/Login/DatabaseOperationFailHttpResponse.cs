using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code - database operation
///     fail http response.
/// </summary>
internal sealed class DatabaseOperationFailHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    internal DatabaseOperationFailHttpResponse()
    {
        HttpCode = StatusCodes.Status500InternalServerError;
        AppCode = BaseAppCode.SERVER_ERROR;
        ErrorMessages =
        [
            "Database operations failed."
        ];
    }
}
