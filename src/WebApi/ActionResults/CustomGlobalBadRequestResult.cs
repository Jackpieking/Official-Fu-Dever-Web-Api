using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.ApiReturnCodes.Base;
using WebApi.Commons;

namespace WebApi.ActionResults;

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
            ErrorMessages = new List<string>(capacity: 1)
            {
                "Invalid inputs are found."
            }
        };

        ObjectResult badResult = new(value: problemDetails)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        await badResult.ExecuteResultAsync(context: context);
    }
}
