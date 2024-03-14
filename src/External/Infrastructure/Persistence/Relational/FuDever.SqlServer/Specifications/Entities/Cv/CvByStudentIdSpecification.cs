using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Cv;

namespace FuDever.SqlServer.Specifications.Entities.Cv;

/// <summary>
///     Represent implementation of cv by student id specification.
/// </summary>
internal sealed class CvByStudentIdSpecification :
    BaseSpecification<Domain.Entities.Cv>,
    ICvByStudentIdSpecification
{
    internal CvByStudentIdSpecification(string studentId)
    {
        WhereExpression = cv => cv.StudentId.Equals(studentId);
    }
}
