using FuDever.Application.Features.Major.GetAllMajors;
using System;
using System.Collections.Generic;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllMajors.Others;

/// <summary>
///     Http response manager for get all majors feature.
/// </summary>
internal sealed class GetAllMajorsHttpResponseManager
{
    private readonly Dictionary<
        GetAllMajorsResponseStatusCode,
        Func<
            GetAllMajorsRequest,
            GetAllMajorsResponse,
            IGetAllMajorsHttpResponse>>
                _dictionary;

    internal GetAllMajorsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllMajorsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllMajorsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllMajorsRequest,
        GetAllMajorsResponse,
        IGetAllMajorsHttpResponse>
            Resolve(GetAllMajorsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
