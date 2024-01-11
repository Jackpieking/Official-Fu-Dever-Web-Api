using Domain.Specifications.Base;
using Domain.Specifications.Entities.Hobby;

namespace Persistence.SqlServer2016.Specifications.Entities.Hobby;

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
        SelectExpression = hobby => new()
        {
            Id = hobby.Id,
            Name = hobby.Name
        };

        return this;
    }

    public ISelectFieldsFromHobbySpecification Ver2()
    {
        SelectExpression = hobby => new()
        {
            Id = hobby.Id,
            Name = hobby.Name,
            RemovedAt = hobby.RemovedAt,
            RemovedBy = hobby.RemovedBy
        };

        return this;
    }


    public ISelectFieldsFromHobbySpecification Ver3()
    {
        SelectExpression = hobby => new()
        {
            Id = hobby.Id
        };

        return this;
    }
}
