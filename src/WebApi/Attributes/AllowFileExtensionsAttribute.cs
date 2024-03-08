using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes;

/// <summary>
///     Attribute to validate the extension of file.
/// </summary>
[AttributeUsage(
    validOn: AttributeTargets.All,
    AllowMultiple = false)]
internal sealed class AllowFileExtensionsAttribute : ValidationAttribute
{
    /// <summary>
    ///     Gets and sets the allowed file extension.
    /// </summary>
    /// <value></value>
    public string[] Extensions { get; set; }

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
        if (value is not IFormFile file)
        {
            return false;
        }

        var inputFileExtension = GetFileExtension(fileName: file.FileName);

        return Array.Exists(
            array: Extensions,
            match: allowedExtension =>
                allowedExtension.Equals(value: inputFileExtension));
    }

    private static string GetFileExtension(string fileName)
    {
        var fileExtensions = fileName.Split(separator: ".");

        return fileExtensions[^1];
    }
}
