using System;
using System.Collections.Generic;

namespace Persistence.RelationalDatabase.SqlServer.Commons;

/// <summary>
///     Represent set of constant.
/// </summary>
internal static class CommonConstant
{
    internal static class DbDataType
    {
        internal const string DATETIME_2 = "DATETIME2";

        internal const string NVARCHAR_MAX = "NVARCHAR(MAX)";

        /// <summary>
        ///     Nvarchar datatype resolver.
        /// </summary>
        internal static class NvarcharGenerator
        {
            private static readonly Dictionary<int, string> _storage = [];
            private const string NvarcharDataTypeName = "NVARCHAR";

            /// <summary>
            ///     Generate nvarchar datatype with given length.
            /// </summary>
            /// <param name="length">
            ///     Length of nvarchar.
            /// </param>
            /// <returns>
            ///     The old instance if it is already existed
            ///     or the new one.
            /// </returns>
            /// <remarks>
            ///     The extension cannot generate max length.
            /// </remarks>
            internal static string Get(int length)
            {
                if (_storage.TryGetValue(
                    key: length,
                    value: out var value))
                {
                    return value;
                }

                var newValue = $"{NvarcharDataTypeName}({length})";

                _storage.Add(
                    key: length,
                    value: newValue);

                return newValue;
            }
        }
    }

    internal static class DbDefaultValue
    {
        internal static readonly DateTime MIN_DATE_TIME = DateTime.MinValue.ToUniversalTime();
    }

    internal static class DbCollation
    {
        internal const string SQL_LATIN1_GENERAL_CP1_CS_AS = "SQL_Latin1_General_CP1_CS_AS";
    }
}
