using FuDever.Application.Features.Role.GetAllTemporarilyRemovedRoles;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllTemporarilyRemovedRoles.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Role.GetAllTemporarilyRemovedRoles;

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
