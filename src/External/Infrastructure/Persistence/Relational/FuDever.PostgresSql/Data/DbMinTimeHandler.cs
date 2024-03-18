using FuDever.Application.Interfaces.Data;
using FuDever.PostgresSql.Commons;
using System;

namespace FuDever.PostgresSql.Data;

/// <summary>
///     Implementation of database min date time handler.
/// </summary>
internal sealed class DbMinTimeHandler : IDbMinTimeHandler
{
    public DateTime Get()
    {
        return CommonConstant.DbDefaultValue.MIN_DATE_TIME;
    }
}
