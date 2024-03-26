using FuDever.Application.Features.Hobby.GetAllTemporarilyRemovedHobbies;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.EntityHttpResponse.Base;
using FuDever.WebApi.EntityHttpResponse.Entities.Hobby.GetAllTemporarilyRemovedHobbies.Others;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Hobby.GetAllTemporarilyRemovedHobbies;

/// <summary>
///     Get all temporarily removed hobbies response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllTemporarilyRemovedHobbiesHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllTemporarilyRemovedHobbiesResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundTemporarilyRemovedHobbies;
    }
}
