using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Project;

public sealed class ProjectByIdSpecification :
    GenericSpecification<ProjectEntity>
{
    public ProjectByIdSpecification(Guid projectId)
    {
        Criteria = project => project.Id == projectId;
    }
}
