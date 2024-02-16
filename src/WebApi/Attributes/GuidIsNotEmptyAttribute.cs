using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes;

/// <summary>
///     Attribute to check if the input guid
///     is not empty.
/// </summary>
internal sealed class GuidIsNotEmptyAttribute : ValidationAttribute
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
        var canParsed = Guid.TryParse(
            input: value.ToString(),
            result: out var newGuid);

        if (!canParsed ||
            newGuid == Guid.Empty)
        {
            return false;
        }

        return true;
    }
}