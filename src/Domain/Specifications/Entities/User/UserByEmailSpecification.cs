using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserByEmailSpecification :
    GenericSpecification<AppUserEntity>
{
    public UserByEmailSpecification(string email)
    {
        Criteria = user => EF.Functions
            .Collate(
                user.Email,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(email);
    }
}
