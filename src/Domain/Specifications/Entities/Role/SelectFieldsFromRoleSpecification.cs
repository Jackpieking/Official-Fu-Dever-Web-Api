using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Role;

public sealed class SelectFieldsFromRoleSpecification :
    GenericSpecification<AppRoleEntity>
{
    public SelectFieldsFromRoleSpecification Ver1()
    {
        SelectExpression = role => new()
        {
            Id = role.Id,
            Name = role.Name
        };

        return this;
    }

    public SelectFieldsFromRoleSpecification Ver2()
    {
        SelectExpression = role => new()
        {
            Id = role.Id,
            Name = role.Name,
            DeletedAt = role.DeletedAt,
            DeletedBy = role.DeletedBy
        };

        return this;
    }
}
