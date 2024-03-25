using FuDever.Domain.EntityBuilders.Project;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Project;

namespace FuDever.SqlServer.Specifications.Entities.Project;

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
        ProjectForDatabaseRetrievingBuilder builder = new();

        SelectExpression = project => builder
            .WithId(project.Id)
            .WithTitle(project.Title)
            .WithDescription(project.Description)
            .WithSourceCodeUrl(project.SourceCodeUrl)
            .WithDemoUrl(project.DemoUrl)
            .WithThumbnailUrl(project.ThumbnailUrl)
            .WithCreatedAt(project.CreatedAt)
            .WithUpdatedAt(project.UpdatedAt)
            .Complete();

        return this;
    }
}
