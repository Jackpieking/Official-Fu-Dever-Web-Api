using System.Collections.Generic;
using System;
using FuDever.Application.Features.Skill.RemoveSkillTemporarilyBySkillId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Skill.RemoveSkillTemporarilyBySkillId.Others;

/// <summary>
///     Http response manager for remove skill
///     temporarily by skill id feature.
/// </summary>
internal sealed class RemoveSkillTemporarilyBySkillIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveSkillTemporarilyBySkillIdResponseStatusCode,
        Func<
            RemoveSkillTemporarilyBySkillIdRequest,
            RemoveSkillTemporarilyBySkillIdResponse,
            IRemoveSkillTemporarilyBySkillIdHttpResponse>>
                _dictionary;

    internal RemoveSkillTemporarilyBySkillIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveSkillTemporarilyBySkillIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveSkillTemporarilyBySkillIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveSkillTemporarilyBySkillIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveSkillTemporarilyBySkillIdResponseStatusCode.SKILL_IS_NOT_FOUND,
            value: (request, _) => new SkillIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveSkillTemporarilyBySkillIdResponseStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new SkillIsAlreadyTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveSkillTemporarilyBySkillIdRequest,
        RemoveSkillTemporarilyBySkillIdResponse,
        IRemoveSkillTemporarilyBySkillIdHttpResponse>
            Resolve(RemoveSkillTemporarilyBySkillIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
