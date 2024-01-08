using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Project.Manager.Contracts;

public interface IProjectSpecificationManager
{
    NoTrackingOnProjectSpecification NoTrackingOnProjectSpecification { get; }

    ProjectByIdSpecification ProjectByIdSpecification(Guid projectId);

    ProjectByUserIdSpecification ProjectByUserIdSpecification(Guid projectId);

    ProjectByTitleSpecification ProjectByTitleSpecification(string projectTitle);

    SelectFieldsFromProjectSpecification SelectFieldsFromProjectSpecification { get; }
}
