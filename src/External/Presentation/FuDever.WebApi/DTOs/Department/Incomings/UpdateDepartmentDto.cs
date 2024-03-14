using FuDever.WebApi.Attributes;
using FuDever.WebApi.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace FuDever.WebApi.DTOs.Department.Incomings;

public sealed class UpdateDepartmentDto : IDtoNormalization
{
    [Required]
    [StringIsNotNullOrWhiteSpace]
    [MaxLength(
        length: Domain.Entities.Department.Metadata.Name.MaxLength,
        ErrorMessage = "Too much chars on department name !!")]
    [MinLength(
        length: Domain.Entities.Department.Metadata.Name.MinLength,
        ErrorMessage = "Less than min length of department name !!")]
    public string NewDepartmentName { get; set; }

    public void NormalizeAllProperties()
    {
        NewDepartmentName = NewDepartmentName.Trim();
    }
}