using FuDever.Application.Features.Major.GetAllMajorsByMajorName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllMajorsByMajorName.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllMajorsByMajorName;

/// <summary>
///     Get all majors by major name response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllMajorsByMajorNameHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllMajorsByMajorNameResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundMajors;
    }
}
