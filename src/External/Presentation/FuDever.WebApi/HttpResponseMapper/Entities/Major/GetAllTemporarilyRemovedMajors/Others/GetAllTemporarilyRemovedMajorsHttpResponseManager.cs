using System.Collections.Generic;
using System;
using FuDever.Application.Features.Major.GetAllTemporarilyRemovedMajors;

namespace FuDever.WebApi.HttpResponseMapper.Entities.Major.GetAllTemporarilyRemovedMajors.Others;

/// <summary>
///     Http response manager for get all
///     temporarily removed majors feature.
/// </summary>
internal sealed class GetAllTemporarilyRemovedMajorsHttpResponseManager
{
    private readonly Dictionary<
        GetAllTemporarilyRemovedMajorsResponseStatusCode,
        Func<
            GetAllTemporarilyRemovedMajorsRequest,
            GetAllTemporarilyRemovedMajorsResponse,
            IGetAllTemporarilyRemovedMajorsHttpResponse>>
                _dictionary;

    internal GetAllTemporarilyRemovedMajorsHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: GetAllTemporarilyRemovedMajorsResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: GetAllTemporarilyRemovedMajorsResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));
    }

    internal Func<
        GetAllTemporarilyRemovedMajorsRequest,
        GetAllTemporarilyRemovedMajorsResponse,
        IGetAllTemporarilyRemovedMajorsHttpResponse>
            Resolve(GetAllTemporarilyRemovedMajorsResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}