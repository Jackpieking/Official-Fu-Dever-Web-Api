using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class SplitQueryOnUserSpecification :
    GenericSpecification<AppUserEntity>
{
    public SplitQueryOnUserSpecification()
    {
        IsAsSplitQuery = true;
    }
}
