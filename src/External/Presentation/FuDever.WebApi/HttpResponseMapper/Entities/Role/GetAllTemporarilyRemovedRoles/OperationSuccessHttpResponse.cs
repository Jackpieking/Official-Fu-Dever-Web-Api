using FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Role.GetAllTemporarilyRemovedRoles.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Role.GetAllTemporarilyRemovedRoles;

/// <summary>
///     Get all temporarily removed roles response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllTemporarilyRemovedRolesHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllTemporarilyRemovedRolesResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundTemporarilyRemovedRoles;
    }
}
