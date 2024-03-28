using System.Collections.Generic;
using System;
using FuDever.Application.Features.Skill.RemoveSkillPermanentlyBySkillId;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Skill.RemoveSkillPermanentlyBySkillId.Others;

/// <summary>
///     Http response manager for remove skill
///     permanently by skill id feature.
/// </summary>
internal sealed class RemoveSkillPermanentlyBySkillIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveSkillPermanentlyBySkillIdResponseStatusCode,
        Func<
            RemoveSkillPermanentlyBySkillIdRequest,
            RemoveSkillPermanentlyBySkillIdResponse,
            IRemoveSkillPermanentlyBySkillIdHttpResponse>>
                _dictionary;

    internal RemoveSkillPermanentlyBySkillIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveSkillPermanentlyBySkillIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveSkillPermanentlyBySkillIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveSkillPermanentlyBySkillIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveSkillPermanentlyBySkillIdResponseStatusCode.SKILL_IS_NOT_FOUND,
            value: (request, _) => new SkillIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveSkillPermanentlyBySkillIdResponseStatusCode.SKILL_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new SkillIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveSkillPermanentlyBySkillIdRequest,
        RemoveSkillPermanentlyBySkillIdResponse,
        IRemoveSkillPermanentlyBySkillIdHttpResponse>
            Resolve(RemoveSkillPermanentlyBySkillIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
