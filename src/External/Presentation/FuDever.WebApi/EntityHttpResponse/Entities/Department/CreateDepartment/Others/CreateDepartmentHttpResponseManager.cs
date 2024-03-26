using FuDever.Application.Features.Department.CreateDepartment;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.CreateDepartment.Others;

/// <summary>
///     Http response manager for create department feature.
/// </summary>
internal sealed class CreateDepartmentHttpResponseManager
{
    private readonly Dictionary<
        CreateDepartmentResponseStatusCode,
        Func<
            CreateDepartmentRequest,
            CreateDepartmentResponse,
            ICreateDepartmentHttpResponse>>
                _dictionary;

    internal CreateDepartmentHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: CreateDepartmentResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: CreateDepartmentResponseStatusCode.OPERATION_SUCCESS,
            value: (_, _) => new OperationSuccessHttpResponse());

        _dictionary.Add(
            key: CreateDepartmentResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: CreateDepartmentResponseStatusCode.DEPARTMENT_IS_ALREADY_TEMPORARILY_REMOVED,
            value: (request, _) => new DepartmentIsAlreadyTemporarilyRemovedHttpResponse(request: request));

        _dictionary.Add(
            key: CreateDepartmentResponseStatusCode.DEPARTMENT_ALREADY_EXISTS,
            value: (request, _) => new DepartmentAlreadyExistsHttpResponse(request: request));
    }

    internal Func<
        CreateDepartmentRequest,
        CreateDepartmentResponse,
        ICreateDepartmentHttpResponse>
            Resolve(CreateDepartmentResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
