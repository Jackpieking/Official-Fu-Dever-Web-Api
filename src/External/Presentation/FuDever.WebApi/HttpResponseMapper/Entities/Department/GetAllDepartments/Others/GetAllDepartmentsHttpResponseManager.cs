using FuDever.Application.Features.Department.GetAllDepartments;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Department.GetAllDepartments.Others;

/// <summary>
///     Http response manager for get all departments feature.
/// </summary>
internal sealed class GetAllDepartmentsHttpResponseManager
{
    private readonly Dictionary<
        GetAllDepartmentsResponseStatusCode,
        Func<
            GetAllDepartmentsRequest,
            GetAllDepartmentsResponse,
            IGetAllDepartmentsHttpResponse>>
                _dictionary;

    internal GetAllDepartmentsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllDepartmentsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllDepartmentsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllDepartmentsRequest,
        GetAllDepartmentsResponse,
        IGetAllDepartmentsHttpResponse>
            Resolve(GetAllDepartmentsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
