using System.Collections.Generic;
using System;
using FuDever.Application.Features.Skill.GetAllTemporarilyRemovedSkills;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.GetAllTemporarilyRemovedSkills.Others;

/// <summary>
///     Http response manager for get all
///     temporarily removed skills feature.
/// </summary>
internal sealed class GetAllTemporarilyRemovedSkillsHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedSkillsResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedSkillsRequest,
            GetAllTemporarilyRemovedSkillsResponse,
            IGetAllTemporarilyRemovedSkillsHttpResponse>>
                _dictionary;

    internal GetAllTemporarilyRemovedSkillsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedSkillsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllTemporarilyRemovedSkillsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllTemporarilyRemovedSkillsRequest,
        GetAllTemporarilyRemovedSkillsResponse,
        IGetAllTemporarilyRemovedSkillsHttpResponse>
            Resolve(GetAllTemporarilyRemovedSkillsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}