using System.Collections.Generic;
using System;
using FuDever.Application.Features.Department.RemoveDepartmentTemporarilyByDepartmentId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.RemoveDepartmentTemporarilyByDepartmentId.Others;

/// <summary>
///     Http response manager for remove department
///     temporarily by department id feature.
/// </summary>
internal sealed class RemoveDepartmentTemporarilyByDepartmentIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode,
        Func<
            RemoveDepartmentTemporarilyByDepartmentIdRequest,
            RemoveDepartmentTemporarilyByDepartmentIdResponse,
            IRemoveDepartmentTemporarilyByDepartmentIdHttpResponse>>
                _dictionary;

    internal RemoveDepartmentTemporarilyByDepartmentIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND,
            value: (request, _) => new DepartmentIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new DepartmentIsAlreadyTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveDepartmentTemporarilyByDepartmentIdRequest,
        RemoveDepartmentTemporarilyByDepartmentIdResponse,
        IRemoveDepartmentTemporarilyByDepartmentIdHttpResponse>
            Resolve(RemoveDepartmentTemporarilyByDepartmentIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
