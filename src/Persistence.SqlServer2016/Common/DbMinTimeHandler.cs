using System;
using Application.Interfaces.Dates;

namespace Persistence.SqlServer2016.Common;

/// <summary>
///     Implementation of database min date time handler.
/// </summary>
public sealed class DbMinTimeHandler : IDbMinTimeHandler
{
    public DateTime Get()
    {
        return CustomConstant.DbDefaultValue.MinDateTime;
    }
}
