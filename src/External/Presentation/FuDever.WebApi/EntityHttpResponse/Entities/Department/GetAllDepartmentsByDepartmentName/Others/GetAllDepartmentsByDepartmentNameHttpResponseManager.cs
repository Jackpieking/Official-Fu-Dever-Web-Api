using System.Collections.Generic;
using System;
using FuDever.Application.Features.Department.GetAllDepartmentsByDepartmentName;

namespace FuDever.WebApi.EntityHttpResponse.Entities.Department.GetAllDepartmentsByDepartmentName.Others;

/// <summary>
///     Http response manager for get all departments
///     by department name feature.
/// </summary>
internal sealed class GetAllDepartmentsByDepartmentNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllDepartmentsByDepartmentNameResponseStatusCode,
        Func<
            GetAllDepartmentsByDepartmentNameRequest,
            GetAllDepartmentsByDepartmentNameResponse,
            IGetAllDepartmentsByDepartmentNameHttpResponse>>
                _dictionary;

    internal GetAllDepartmentsByDepartmentNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllDepartmentsByDepartmentNameResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllDepartmentsByDepartmentNameResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllDepartmentsByDepartmentNameRequest,
        GetAllDepartmentsByDepartmentNameResponse,
        IGetAllDepartmentsByDepartmentNameHttpResponse>
            Resolve(GetAllDepartmentsByDepartmentNameResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
