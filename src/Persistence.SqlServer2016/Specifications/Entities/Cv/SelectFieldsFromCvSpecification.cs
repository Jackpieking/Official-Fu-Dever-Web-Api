using Domain.Specifications.Base;
using Domain.Specifications.Entities.Cv;

namespace Persistence.SqlServer2016.Specifications.Entities.Cv;

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
        SelectExpression = cv => new()
        {
            Id = cv.Id,
            FullName = cv.FullName,
            Email = cv.Email,
            StudentId = cv.StudentId,
            CvFileId = cv.CvFileId
        };

        return this;
    }
}
