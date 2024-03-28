using FuDever.Application.Features.Skill.CreateSkill;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Skill.CreateSkill.Others;

/// <summary>
///     Http response manager for create skill feature.
/// </summary>
internal sealed class CreateSkillHttpResponseManager
{
    private readonly Dictionary<
        CreateSkillResponseStatusCode,
        Func<
            CreateSkillRequest,
            CreateSkillResponse,
            ICreateSkillHttpResponse>>
                _dictionary;

    internal CreateSkillHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateSkillResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: CreateSkillResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: CreateSkillResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: CreateSkillResponseStatusCode.SKILL_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new SkillIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: CreateSkillResponseStatusCode.SKILL_ALREADY_EXISTS,
            value: (request, _) => new SkillAlreadyExistsHttpResponse(request: request));
    }

    internal Func<
        CreateSkillRequest,
        CreateSkillResponse,
        ICreateSkillHttpResponse>
            Resolve(CreateSkillResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
