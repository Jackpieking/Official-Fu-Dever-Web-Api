using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;

namespace FuDever.SqlServer.Specifications.Entities.Major;

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
        SelectExpression = major => Domain.Entities.Major.InitVer2(
            major.Id,
            major.Name);

        return this;
    }

    public ISelectFieldsFromMajorSpecification Ver2()
    {
        SelectExpression = major => Domain.Entities.Major.InitVer1(
            major.Id,
            major.Name,
            major.RemovedAt,
            major.RemovedBy);

        return this;
    }

    public ISelectFieldsFromMajorSpecification Ver3()
    {
        SelectExpression = major => Domain.Entities.Major.InitVer3(major.Name);

        return this;
    }
}
