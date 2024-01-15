using System;

namespace Persistence.SqlServer2016.Common;

/// <summary>
///     Represent set of constant.
/// </summary>
internal static class CommonConstant
{
    internal static class DbDataType
    {
        internal const string DATETIME = "DATETIME";

        internal const string NVARCHAR_MAX = "NVARCHAR(MAX)";

        internal const string NVARCHAR_100 = "NVARCHAR(100)";

        internal const string NVARCHAR_200 = "NVARCHAR(100)";

        internal const string NVARCHAR_50 = "NVARCHAR(50)";

        internal const string NVARCHAR_30 = "NVARCHAR(30)";
    }

    internal static class DbDefaultValue
    {
        internal static readonly DateTime MIN_DATE_TIME = new(
            year: 1753,
            month: 1,
            day: 1,
            hour: 0,
            minute: 0,
            second: 0,
            kind: DateTimeKind.Utc);
    }

    internal static class DbCollation
    {
        internal const string SQL_LATIN1_GENERAL_CP1_CS_AS = "SQL_Latin1_General_CP1_CS_AS";
    }
}
