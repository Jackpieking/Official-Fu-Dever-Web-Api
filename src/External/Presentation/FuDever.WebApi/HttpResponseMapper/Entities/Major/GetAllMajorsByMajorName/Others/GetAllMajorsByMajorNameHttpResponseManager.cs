using System.Collections.Generic;
using System;
using FuDever.Application.Features.Major.GetAllMajorsByMajorName;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllMajorsByMajorName.Others;

/// <summary>
///     Http response manager for get all majors
///     by major name feature.
/// </summary>
internal sealed class GetAllMajorsByMajorNameHttpResponseManager
{
    private readonly Dictionary<
        GetAllMajorsByMajorNameResponseStatusCode,
        Func<
            GetAllMajorsByMajorNameRequest,
            GetAllMajorsByMajorNameResponse,
            IGetAllMajorsByMajorNameHttpResponse>>
                _dictionary;

    internal GetAllMajorsByMajorNameHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllMajorsByMajorNameResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllMajorsByMajorNameResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllMajorsByMajorNameRequest,
        GetAllMajorsByMajorNameResponse,
        IGetAllMajorsByMajorNameHttpResponse>
            Resolve(GetAllMajorsByMajorNameResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
