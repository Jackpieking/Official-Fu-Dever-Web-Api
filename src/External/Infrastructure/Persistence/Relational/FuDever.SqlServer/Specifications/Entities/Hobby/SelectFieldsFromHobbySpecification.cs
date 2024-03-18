using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.Hobby;

namespace FuDever.SqlServer.Specifications.Entities.Hobby;

/// <summary>
///     Represent implementation of select fields from the "Hobbies"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromHobbySpecification :
    BaseSpecification<Domain.Entities.Hobby>,
    ISelectFieldsFromHobbySpecification
{
    public ISelectFieldsFromHobbySpecification Ver1()
    {
        SelectExpression = hobby => Domain.Entities.Hobby.InitFromDatabaseVer1(
            hobby.Id,
            hobby.Name);

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver2()
    {
        SelectExpression = hobby => Domain.Entities.Hobby.InitFromDatabaseVer2(
            hobby.Id,
            hobby.Name,
            hobby.RemovedAt,
            hobby.RemovedBy);

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver3()
    {
        SelectExpression = hobby => Domain.Entities.Hobby.InitFromDatabaseVer3(hobby.Id);

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver4()
    {
        SelectExpression = hobby => Domain.Entities.Hobby.InitFromDatabaseVer4(hobby.Name);

        return this;
    }
}
