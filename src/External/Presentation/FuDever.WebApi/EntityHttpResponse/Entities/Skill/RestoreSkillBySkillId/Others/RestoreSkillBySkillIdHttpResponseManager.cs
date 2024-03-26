using System.Collections.Generic;
using System;
using FuDever.Application.Features.Skill.RestoreSkillBySkillId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.RestoreSkillBySkillId.Others;

/// <summary>
///     Http response manager for restore skill
///     by skill id feature.
/// </summary>
internal sealed class RestoreSkillBySkillIdHttpResponseManager
{
    private readonly Dictionary<
    RestoreSkillBySkillIdResponseStatusCode,
    Func<
        RestoreSkillBySkillIdRequest,
        RestoreSkillBySkillIdResponse,
        IRestoreSkillBySkillIdHttpResponse>>
            _dictionary;

    internal RestoreSkillBySkillIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RestoreSkillBySkillIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RestoreSkillBySkillIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RestoreSkillBySkillIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RestoreSkillBySkillIdResponseStatusCode.SKILL_IS_NOT_FOUND,
            value: (request, _) => new SkillIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RestoreSkillBySkillIdResponseStatusCode.SKILL_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new SkillIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RestoreSkillBySkillIdRequest,
        RestoreSkillBySkillIdResponse,
        IRestoreSkillBySkillIdHttpResponse>
            Resolve(RestoreSkillBySkillIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
