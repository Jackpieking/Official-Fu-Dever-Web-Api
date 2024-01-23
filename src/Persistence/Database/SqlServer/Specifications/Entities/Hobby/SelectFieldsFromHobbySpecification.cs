using Domain.Specifications.Base;
using Domain.Specifications.Entities.Hobby;

namespace Persistence.Database.SqlServer.Specifications.Entities.Hobby;

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
        SelectExpression = hobby => Domain.Entities.Hobby.Init(
            hobby.Id,
            hobby.Name);

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver2()
    {
        SelectExpression = hobby => Domain.Entities.Hobby.Init(
            hobby.Id,
            hobby.Name,
            hobby.RemovedAt,
            hobby.RemovedBy);

        return this;
    }


    public ISelectFieldsFromHobbySpecification Ver3()
    {
        SelectExpression = hobby => Domain.Entities.Hobby.Init(hobby.Id);

        return this;
    }
}
