using FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllTemporarilyRemovedMajors.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllTemporarilyRemovedMajors;

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
