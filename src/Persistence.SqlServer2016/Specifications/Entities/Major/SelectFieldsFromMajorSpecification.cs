using Domain.Specifications.Base;
using Domain.Specifications.Entities.Major;

namespace Persistence.SqlServer2016.Specifications.Entities.Major;

/// <summary>
///     Represent implementation of select fields from the "Majors"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromMajorSpecification :
    BaseSpecification<Domain.Entities.Major>,
    ISelectFieldsFromMajorSpecification
{
    public ISelectFieldsFromMajorSpecification Ver1()
    {
        SelectExpression = major => new()
        {
            Id = major.Id,
            Name = major.Name
        };

        return this;
    }

    public ISelectFieldsFromMajorSpecification Ver2()
    {
        SelectExpression = major => new()
        {
            Id = major.Id,
            Name = major.Name,
            RemovedAt = major.RemovedAt,
            RemovedBy = major.RemovedBy
        };

        return this;
    }
}
