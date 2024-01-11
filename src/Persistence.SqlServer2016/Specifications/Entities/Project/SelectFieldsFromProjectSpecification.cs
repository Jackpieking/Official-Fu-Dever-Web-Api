using Domain.Specifications.Base;
using Domain.Specifications.Entities.Project;

namespace Persistence.SqlServer2016.Specifications.Entities.Project;

/// <summary>
///     Represent implementation of select fields from the "Projects"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromProjectSpecification :
    BaseSpecification<Domain.Entities.Project>,
    ISelectFieldsFromProjectSpecification
{
    public ISelectFieldsFromProjectSpecification Ver1()
    {
        SelectExpression = project => new()
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            SourceCodeUrl = project.SourceCodeUrl,
            DemoUrl = project.DemoUrl,
            ThumbnailUrl = project.ThumbnailUrl,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };

        return this;
    }
}
