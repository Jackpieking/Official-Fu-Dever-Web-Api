using FuDever.Application.Features.Hobby.GetAllHobbies;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.GetAllHobbies.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.GetAllHobbies;

/// <summary>
///     Get all hobbies response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllHobbiesHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllHobbiesResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundHobbies;
    }
}
