using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Position;

public sealed class SelectFieldsFromPositionSpecification :
    GenericSpecification<PositionEntity>
{
    public SelectFieldsFromPositionSpecification Ver1()
    {
        SelectExpression = position => new()
        {
            Id = position.Id,
            Name = position.Name
        };

        return this;
    }

    public SelectFieldsFromPositionSpecification Ver2()
    {
        SelectExpression = position => new()
        {
            Id = position.Id,
            Name = position.Name,
            DeletedAt = position.DeletedAt,
            DeletedBy = position.DeletedBy
        };

        return this;
    }
}
