using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Major;

namespace FuDever.PostgresSql.Specifications.Entities.Major;

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
        SelectExpression = major => Domain.Entities.Major.InitFromDatabaseVer1(
            major.Id,
            major.Name);

        return this;
    }

    public ISelectFieldsFromMajorSpecification Ver2()
    {
        SelectExpression = major => Domain.Entities.Major.InitFromDatabaseVer3(
            major.Id,
            major.Name,
            major.RemovedAt,
            major.RemovedBy);

        return this;
    }

    public ISelectFieldsFromMajorSpecification Ver3()
    {
        SelectExpression = major => Domain.Entities.Major.InitFromDatabaseVer2(major.Name);

        return this;
    }
}
