using Domain.Specifications.Base;
using Domain.Specifications.Entities.Cv;

namespace Persistence.Database.SqlServer.Specifications.Entities.Cv;

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
