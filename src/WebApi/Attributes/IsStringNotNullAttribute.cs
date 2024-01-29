using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes;

/// <summary>
///     Attribute to check if the input string
///     is not null or contain null string value.
/// </summary>
internal sealed class IsStringNotNullAttribute : ValidationAttribute
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
        if (Equals(objA: value, objB: null) ||
            value.Equals(obj: "null"))
        {
            return false;
        }

        return true;
    }
}
