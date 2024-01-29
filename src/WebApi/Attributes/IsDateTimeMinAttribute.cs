using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Application.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Attributes;

/// <summary>
///     Attribute to check if the input string
///     is in date time format.
/// </summary>
internal sealed class IsDateTimeMinAttribute : ValidationAttribute
{
    /// <summary>
    ///     Entry to validate.
    /// </summary>
    /// <param name="value">
    ///     Value to validate.
    /// </param>
    /// <param name="validationContext">
    ///     Context for validation.
    /// </param>
    /// <returns>
    ///     Validation result.
    /// </returns>
    protected override ValidationResult IsValid(
        object value,
        ValidationContext validationContext)
    {
        if (Equals(objA: value, objB: null))
        {
            return new(errorMessage: false.ToString());
        }

        var canParsed = DateTime.TryParse(
            s: value.ToString(),
            provider: CultureInfo.InvariantCulture,
            result: out var newDateTime);

        var dbMinTimeHandler = validationContext.GetRequiredService<IDbMinTimeHandler>();

        if (!canParsed ||
            newDateTime.ToUniversalTime() < dbMinTimeHandler.Get())
        {
            return new(errorMessage: false.ToString());
        }

        return ValidationResult.Success;
    }
}
