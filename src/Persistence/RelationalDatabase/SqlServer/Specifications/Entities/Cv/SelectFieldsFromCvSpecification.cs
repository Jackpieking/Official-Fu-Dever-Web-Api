using Domain.Specifications.Base;
using Domain.Specifications.Entities.Cv;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.Cv;

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
        SelectExpression = cv => Domain.Entities.Cv.InitVer1(
            cv.Id,
            cv.StudentFullName,
            cv.StudentEmail,
            cv.StudentId,
            cv.StudentCvFileId);

        return this;
    }
}
