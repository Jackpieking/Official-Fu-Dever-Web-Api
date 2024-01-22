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
        SelectExpression = project => Domain.Entities.Project.Init(
            project.Id,
            project.Title,
            project.Description,
            project.SourceCodeUrl,
            project.DemoUrl,
            project.ThumbnailUrl,
            project.CreatedAt,
            project.UpdatedAt);

        return this;
    }
}
