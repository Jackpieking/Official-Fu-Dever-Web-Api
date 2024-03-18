using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Cv;

namespace FuDever.PostgresSql.Specifications.Entities.Cv;

/// <summary>
///     Represent implementation of cv by student email specification.
/// </summary>
internal sealed class CvByEmailSpecification :
    BaseSpecification<Domain.Entities.Cv>,
    ICvByEmailSpecification
{
    internal CvByEmailSpecification(string email)
    {
        WhereExpression = cv => cv.StudentEmail.Equals(email);
    }
}
