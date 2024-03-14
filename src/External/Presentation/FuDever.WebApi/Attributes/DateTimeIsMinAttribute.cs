using FuDever.Application.Interfaces.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FuDever.WebApi.Attributes;

/// <summary>
///     Attribute to check if the input string
///     is in date time format.
/// </summary>
[AttributeUsage(
    validOn: AttributeTargets.All,
    AllowMultiple = false)]
internal sealed class DateTimeIsMinAttribute : ValidationAttribute
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
