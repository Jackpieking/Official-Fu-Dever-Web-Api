using System;

namespace Persistence.SqlServer2016.Common;

public static class CustomConstant
{
    public static class DbDataType
    {
        public const string DATETIME = "DATETIME";

        public const string NVARCHAR_MAX = "NVARCHAR(MAX)";

        public const string NVARCHAR_100 = "NVARCHAR(100)";

        public const string NVARCHAR_200 = "NVARCHAR(100)";

        public const string NVARCHAR_50 = "NVARCHAR(50)";

        public const string NVARCHAR_30 = "NVARCHAR(30)";
    }

    public static class DbDefaultValue
    {
        public static readonly DateTime MinDateTime = new(
            year: 1753,
            month: 1,
            day: 1,
            hour: 0,
            minute: 0,
            second: 0,
            kind: DateTimeKind.Utc);
    }
}
