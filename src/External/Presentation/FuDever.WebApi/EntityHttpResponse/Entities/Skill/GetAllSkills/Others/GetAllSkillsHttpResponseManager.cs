using FuDever.Application.Features.Skill.GetAllSkills;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllSkills.Others;

/// <summary>
///     Http response manager for get all skills feature.
/// </summary>
internal sealed class GetAllSkillsHttpResponseManager
{
    private readonly Dictionary<
        GetAllSkillsResponseStatusCode,
        Func<
            GetAllSkillsRequest,
            GetAllSkillsResponse,
            IGetAllSkillsHttpResponse>>
                _dictionary;

    internal GetAllSkillsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllSkillsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllSkillsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllSkillsRequest,
        GetAllSkillsResponse,
        IGetAllSkillsHttpResponse>
            Resolve(GetAllSkillsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
