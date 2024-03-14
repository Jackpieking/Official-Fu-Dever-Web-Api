using FuDever.Application.Interfaces.Data;
using FuDever.SqlServer.Commons;
using System;

namespace FuDever.SqlServer.Data;

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
