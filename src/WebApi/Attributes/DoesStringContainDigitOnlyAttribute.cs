using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApi.Attributes;

/// <summary>
///     Attribute to check if the input string
///     contains any digit.
/// </summary>
internal sealed class DoesStringContainDigitOnlyAttribute : ValidationAttribute
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

        if (!valueAsString.All(predicate: char.IsDigit))
        {
            return false;
        }

        return true;
    }
}

