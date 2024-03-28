using System;
using System.Collections.Generic;

namespace FuDever.WebApi.HttpResponseMapper.Base;

/// <summary>
///     Base http response for all entities http response.
/// </summary>
internal interface IBaseHttpResponse
{
    int HttpCode { get; init; }

    int AppCode { get; init; }

    DateTime ResponseTime { get; init; }

    object Body { get; init; }

    IEnumerable<string> ErrorMessages { get; init; }
}
