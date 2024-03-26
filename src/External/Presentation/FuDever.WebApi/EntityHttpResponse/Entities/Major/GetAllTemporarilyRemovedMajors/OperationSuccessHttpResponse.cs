using FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Major.GetAllTemporarilyRemovedMajors.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Major.GetAllTemporarilyRemovedMajors;

/// <summary>
///     Get all temporarily removed majors response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllTemporarilyRemovedMajorsHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllTemporarilyRemovedMajorsResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundTemporarilyRemovedMajors;
    }
}
