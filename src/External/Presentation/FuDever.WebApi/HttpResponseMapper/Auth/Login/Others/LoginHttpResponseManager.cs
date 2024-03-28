using System;
using System.Collections.Generic;
using FuDever.Application.Features.Auth.Login;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;

/// <summary>
///     Http response manager for login feature.
/// </summary>
internal sealed class LoginHttpResponseManager
{
    private readonly Dictionary<
        LoginResponseStatusCode,
        Func<
            LoginRequest,
            LoginResponse,
            ILoginHttpResponse>>
                _dictionary;

    internal LoginHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: LoginResponseStatusCode.DATABASE_OPERATION_FAIL,
            value: (_, _) => new DatabaseOperationFailHttpResponse());

        _dictionary.Add(
            key: LoginResponseStatusCode.INPUT_VALIDATION_FAIL,
            value: (_, _) => new InputValidationFailHttpResponse());

        _dictionary.Add(
            key: LoginResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) => new OperationSuccessHttpResponse(response: response));

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_EMAIL_IS_NOT_CONFIRMED,
            value: (request, _) => new UserEmailIsNotConfirmedHttpResponse(request: request));

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_IS_LOCKED_OUT,
            value: (request, _) => new UserIsLockedOutHttpResponse(request: request));

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_IS_NOT_APPROVED,
            value: (request, _) => new UserIsNotApprovedHttpResponse(request: request));

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_IS_NOT_FOUND,
            value: (request, _) => new UserIsNotFoundHttpResponse(request: request));

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_IS_TEMPORARILY_REMOVED,
            value: (request, _) => new UserEmailIsNotConfirmedHttpResponse(request: request));

        _dictionary.Add(
            key: LoginResponseStatusCode.USER_PASSWORD_IS_NOT_CORRECT,
            value: (request, _) => new UserPasswordIsNotCorrectHttpResponse(request: request));
    }

    internal Func<
        LoginRequest,
        LoginResponse,
        ILoginHttpResponse>
            Resolve(LoginResponseStatusCode statusCode)
    {
        return _dictionary.GetValueOrDefault(key: statusCode);
    }
}
