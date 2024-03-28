using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;
using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login;

/// <summary>
///     Login response status code
///     - input validation fail http
///     response.
/// </summary>
internal sealed class InputValidationFailHttpResponse :
    BaseHttpResponse,
    ILoginHttpResponse
{
    internal InputValidationFailHttpResponse()
    {
        HttpCode = StatusCodes.Status400BadRequest;
        AppCode = BaseAppCode.SERVER_ERROR;
        ErrorMessages =
        [
            "Input validation fail. Please check your inputs and try again."
        ];
    }
}
