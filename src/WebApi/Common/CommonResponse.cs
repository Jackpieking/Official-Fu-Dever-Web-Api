using System;
using System.Linq;
using WebApi.ApiReturnCodes.Base;

namespace WebApi.Common;

/// <summary>
///     Contain common response for all api.
/// </summary>
/// <remarks>
///     All api response format must be this format.
/// </remarks>
internal sealed class CommonResponse
{
    /// <summary>
    ///     Response body.
    /// </summary>
    public object Body { get; init; } = new();

    /// <summary>
    ///     Response time.
    /// </summary>
    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    /// <summary>
    ///     Response status code.
    /// </summary>
    public int ApiReturnCode { get; init; } = BaseApiReturnCode.SUCCESS;

    /// <summary>
    ///     Response errors messages.
    /// </summary>
    public object ErrorMessages { get; init; } = Enumerable.Empty<object>();
}
