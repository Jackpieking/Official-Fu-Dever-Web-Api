using FuDeverWebApi.DataAccess.Specifications.Entites.Project.Manager.Contracts;
using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Project.Manager.Implementation;

public sealed class ProjectSpecificationManager :
    IProjectSpecificationManager
{
    // Backing fields.
    private NoTrackingOnProjectSpecification _noTrackingOnProjectSpecification;
    private SelectFieldsFromProjectSpecification _selectFieldsFromProjectSpecification;

    public NoTrackingOnProjectSpecification NoTrackingOnProjectSpecification
    {
        get
        {
            _noTrackingOnProjectSpecification ??= new();

            return _noTrackingOnProjectSpecification;
        }
    }

    public SelectFieldsFromProjectSpecification SelectFieldsFromProjectSpecification
    {
        get
        {
            _selectFieldsFromProjectSpecification ??= new();

            return _selectFieldsFromProjectSpecification.Ver1();
        }
    }

    public ProjectByTitleSpecification ProjectByTitleSpecification(string projectTitle)
    {
        return new(projectTitle: projectTitle);
    }

    public ProjectByIdSpecification ProjectByIdSpecification(Guid projectId)
    {
        return new(projectId: projectId);
    }

    public ProjectByUserIdSpecification ProjectByUserIdSpecification(Guid userId)
    {
        return new(userId: userId);
    }
}
