using FuDever.WebApi.ApiReturnCodes.Base;
using FuDever.WebApi.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FuDever.WebApi.ActionResults;

/// <summary>
///     Represent custom validation problem details result.
/// </summary>
internal sealed class CustomGlobalBadRequestResult : IActionResult
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        CommonResponse problemDetails = new()
        {
            ApiReturnCode = BaseApiReturnCode.INVALID_INPUTS,
            ErrorMessages =
            [
                "Invalid inputs are found."
            ]
        };

        ObjectResult badResult = new(value: problemDetails)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        await badResult.ExecuteResultAsync(context: context);
    }
}
