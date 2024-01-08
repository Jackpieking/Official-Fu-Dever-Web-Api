using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Project;

public sealed class ProjectByUserIdSpecification :
    GenericSpecification<ProjectEntity>
{
    public ProjectByUserIdSpecification(Guid userId)
    {
        Criteria = project => project.AuthorId == userId;
    }
}
