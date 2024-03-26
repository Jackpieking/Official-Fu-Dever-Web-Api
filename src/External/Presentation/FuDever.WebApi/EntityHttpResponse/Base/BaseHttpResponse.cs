using FuDever.WebApi.AppCodes.Base;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FuDever.WebApi.EntityHttpResponse.Base;

/// <summary>
///     Base http response for all entities http response.
/// </summary>
/// <remarks>
///     All entity api responses format must be this format.
/// </remarks>
public abstract class BaseHttpResponse : IBaseHttpResponse
{
    [JsonIgnore]
    public int HttpCode { get; init; }

    public int AppCode { get; init; } = BaseAppCode.SUCCESS;

    public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
        dateTime: DateTime.UtcNow,
        destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

    public object Body { get; init; } = new();

    public IEnumerable<string> ErrorMessages { get; init; } = [];
}
