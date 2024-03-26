using System.Collections.Generic;
using System;
using FuDever.Application.Features.Skill.GetAllSkillsBySkillName;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllSkillsBySkillName.Others;

/// <summary>
///     Http response manager for get all skills
///     by skill name feature.
/// </summary>
internal sealed class GetAllSkillsBySkillNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllSkillsBySkillNameResponseStatusCode,
        Func<
            GetAllSkillsBySkillNameRequest,
            GetAllSkillsBySkillNameResponse,
            IGetAllSkillsBySkillNameHttpResponse>>
                _dictionary;

    internal GetAllSkillsBySkillNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllSkillsBySkillNameResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllSkillsBySkillNameResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllSkillsBySkillNameRequest,
        GetAllSkillsBySkillNameResponse,
        IGetAllSkillsBySkillNameHttpResponse>
            Resolve(GetAllSkillsBySkillNameResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
