using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApi.Attributes;

/// <summary>
///     Attribute to check if the input string
///     contains any digit.
/// </summary>
[AttributeUsage(
    validOn: AttributeTargets.All,
    AllowMultiple = false)]
internal sealed class StringContainDigitOnlyAttribute : ValidationAttribute
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
        if (Equals(objA: value, objB: null))
        {
            return false;
        }

        var valueAsString = value as string;

        return valueAsString.All(predicate: char.IsDigit);
    }
}