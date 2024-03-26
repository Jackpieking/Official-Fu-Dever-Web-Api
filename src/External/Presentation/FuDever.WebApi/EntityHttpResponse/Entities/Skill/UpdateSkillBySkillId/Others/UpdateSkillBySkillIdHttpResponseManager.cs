using System.Collections.Generic;
using System;
using FuDever.Application.Features.Skill.UpdateSkillBySkillId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.UpdateSkillBySkillId.Others;

/// <summary>
///     Http response manager for update skill
///     by skill id feature.
/// </summary>
internal sealed class UpdateSkillBySkillIdHttpResponseManager
{
    private readonly Dictionary<
    UpdateSkillBySkillIdResponseStatusCode,
    Func<
        UpdateSkillBySkillIdRequest,
        UpdateSkillBySkillIdResponse,
        IUpdateSkillBySkillIdHttpResponse>>
            _dictionary;

    internal UpdateSkillBySkillIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateSkillBySkillIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: UpdateSkillBySkillIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: UpdateSkillBySkillIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: UpdateSkillBySkillIdResponseStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new SkillIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateSkillBySkillIdResponseStatusCode.SKILL_ALREADY_EXISTS,
            value: (request, _) => new SkillAlreadyExistsHttpResponse(request: request));

        _dictionary.Add(
            key: UpdateSkillBySkillIdResponseStatusCode.SKILL_IS_NOT_FOUND,
            value: (request, _) => new SkillIsNotFoundHttpResponse(request: request));
    }

    internal Func<
        UpdateSkillBySkillIdRequest,
        UpdateSkillBySkillIdResponse,
        IUpdateSkillBySkillIdHttpResponse>
            Resolve(UpdateSkillBySkillIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
