using Application.Interfaces.Data;
using Persistence.RelationalDatabase.SqlServer.Commons;
using System;

namespace Persistence.RelationalDatabase.SqlServer.Data;

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
