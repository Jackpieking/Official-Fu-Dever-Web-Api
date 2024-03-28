using FuDever.Application.Features.Hobby.GetAllHobbiesByHobbyName;
using FuDever.WebApi.AppCodes.Base;
using FuDever.WebApi.HttpResponseMapper.Entities.Hobby.GetAllHobbiesByHobbyName.Others;
using FuDever.WebApi.HttpResponseMapper.Base;
using Microsoft.AspNetCore.Http;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Hobby.GetAllHobbiesByHobbyName;

/// <summary>
///     Get all hobbies by hobby name response status code
///     - operation success http response.
/// </summary>
internal sealed class OperationSuccessHttpResponse :
    BaseHttpResponse,
    IGetAllHobbiesByHobbyNameHttpResponse
{
    internal OperationSuccessHttpResponse(GetAllHobbiesByHobbyNameResponse response)
    {
        HttpCode = StatusCodes.Status200OK;
        AppCode = BaseAppCode.SUCCESS;
        Body = response.FoundHobbies;
    }
}
