namespace WebApi.ApiReturnCodes.Base;

/// <summary>
///     Represent base api return code.
/// </summary>
/// <remarks>
///     All child classes that represent custom
///     api return code must inherit from this.
/// </remarks>
internal abstract class BaseApiReturnCode
{
    internal const int SUCCESS = 0;

    internal const int FAILED = 1;

    internal const int INVALID_INPUTS = 2;
}