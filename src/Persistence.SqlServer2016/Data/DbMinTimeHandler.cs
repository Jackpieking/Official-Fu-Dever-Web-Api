using System;
using Application.Interfaces.Data;
using Persistence.SqlServer2016.Common;

namespace Persistence.SqlServer2016.Data;

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
