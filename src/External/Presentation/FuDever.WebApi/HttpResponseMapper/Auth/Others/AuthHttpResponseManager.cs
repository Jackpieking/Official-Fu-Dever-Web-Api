using FuDever.WebApi.HttpResponseMapper.Auth.Login.Others;

namespace FuDever.WebApi.HttpResponseMapper.Auth.Others;

internal sealed class AuthHttpResponseManager
{
    // Backing fields
    private LoginHttpResponseManager _loginHttpResponseManager;

    internal LoginHttpResponseManager Login
    {
        get
        {
            _loginHttpResponseManager ??= new();

            return _loginHttpResponseManager;
        }
    }
}
