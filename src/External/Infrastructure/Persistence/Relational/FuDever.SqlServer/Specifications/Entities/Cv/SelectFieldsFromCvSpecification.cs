using FuDever.Domain.EntityBuilders.Cv;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Cv;

namespace FuDever.SqlServer.Specifications.Entities.Cv;

/// <summary>
///     Represent implementation of select fields from "Cvs"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromCvSpecification :
    BaseSpecification<Domain.Entities.Cv>,
    ISelectFieldsFromCvSpecification
{
    public ISelectFieldsFromCvSpecification Ver1()
    {
        CvForDatabaseRetrievingBuilder builder = new();

        SelectExpression = cv => builder
            .WithId(cv.Id)
            .WithStudentFullName(cv.StudentFullName)
            .WithStudentEmail(cv.StudentEmail)
            .WithStudentId(cv.StudentId)
            .WithStudentCvFileId(cv.StudentCvFileId)
            .Complete();

        return this;
    }
}
