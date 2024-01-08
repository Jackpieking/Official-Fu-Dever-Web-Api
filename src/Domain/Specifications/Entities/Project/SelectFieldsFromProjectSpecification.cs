using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Project;

public sealed class SelectFieldsFromProjectSpecification :
    GenericSpecification<ProjectEntity>
{
    public SelectFieldsFromProjectSpecification Ver1()
    {
        SelectExpression = project => new()
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            ProjectUrl = project.ProjectUrl,
            DemoUrl = project.DemoUrl,
            ThumbnailUrl = project.ThumbnailUrl,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };

        return this;
    }
}
