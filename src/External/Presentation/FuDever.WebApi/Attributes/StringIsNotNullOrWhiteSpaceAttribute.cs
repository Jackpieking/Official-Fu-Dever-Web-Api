using System;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.Attributes;

/// <summary>
///     Attribute to check if the input string
///     is not null or white space.
/// </summary>
[AttributeUsage(
    validOn: AttributeTargets.All,
    AllowMultiple = false)]
internal sealed class StringIsNotNullOrWhiteSpaceAttribute : ValidationAttribute
{
    /// <summary>
    ///     Entry to validate.
    /// </summary>
    /// <param name="value">
    ///     Value to validate.
    /// </param>
    /// <returns>
    ///     True if success. Otherwise, false.
    /// </returns>
    public override bool IsValid(object value)
    {
        var valueAsString = value as string;

        if (string.IsNullOrWhiteSpace(value: valueAsString) ||
            valueAsString.Equals(obj: "null"))
        {
            return false;
        }

        return true;
    }
}
