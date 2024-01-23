using Application.Interfaces.Data;
using Persistence.Commons;
using System;

namespace Persistence.Database.SqlServer.Data;

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
