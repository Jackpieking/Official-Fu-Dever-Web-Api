using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;
using WebApi.DTOs.Common;

namespace WebApi.DTOs.Department.Incomings;

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
