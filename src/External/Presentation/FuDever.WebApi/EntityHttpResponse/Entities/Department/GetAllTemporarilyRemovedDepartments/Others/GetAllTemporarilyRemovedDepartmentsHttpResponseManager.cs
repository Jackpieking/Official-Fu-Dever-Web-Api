using System.Collections.Generic;
using System;
using FuDever.Application.Features.Department.GetAllTemporarilyRemovedDepartments;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.GetAllTemporarilyRemovedDepartments.Others;

/// <summary>
///     Http response manager for get all
///     temporarily removed departments feature.
/// </summary>
internal sealed class GetAllTemporarilyRemovedDepartmentsHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedDepartmentsResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedDepartmentsRequest,
            GetAllTemporarilyRemovedDepartmentsResponse,
            IGetAllTemporarilyRemovedDepartmentsHttpResponse>>
                _dictionary;

    internal GetAllTemporarilyRemovedDepartmentsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedDepartmentsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllTemporarilyRemovedDepartmentsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllTemporarilyRemovedDepartmentsRequest,
        GetAllTemporarilyRemovedDepartmentsResponse,
        IGetAllTemporarilyRemovedDepartmentsHttpResponse>
            Resolve(GetAllTemporarilyRemovedDepartmentsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}