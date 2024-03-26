using System.Collections.Generic;
using System;
using FuDever.Application.Features.Department.RemoveDepartmentPermanentlyByDepartmentId;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.RemoveDepartmentPermanentlyByDepartmentId.Others;

/// <summary>
///     Http response manager for remove department
///     permanently by department id feature.
/// </summary>
internal sealed class RemoveDepartmentPermanentlyByDepartmentIdHttpResponseManager
{
    private readonly Dictionary<
        RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode,
        Func<
            RemoveDepartmentPermanentlyByDepartmentIdRequest,
            RemoveDepartmentPermanentlyByDepartmentIdResponse,
            IRemoveDepartmentPermanentlyByDepartmentIdHttpResponse>>
                _dictionary;

    internal RemoveDepartmentPermanentlyByDepartmentIdHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_FOUND,
            value: (request, _) => new DepartmentIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode.DEPARTMENT_IS_NOT_TEMPORARILY_REMOVED,
            value: (request, _) => new DepartmentIsNotTemporarilyRemovedHttpResponse(request: request));
    }

    internal Func<
        RemoveDepartmentPermanentlyByDepartmentIdRequest,
        RemoveDepartmentPermanentlyByDepartmentIdResponse,
        IRemoveDepartmentPermanentlyByDepartmentIdHttpResponse>
            Resolve(RemoveDepartmentPermanentlyByDepartmentIdResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
