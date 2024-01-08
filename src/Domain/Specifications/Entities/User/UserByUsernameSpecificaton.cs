using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User;

public sealed class UserByUsernameSpecificaton :
    GenericSpecification<AppUserEntity>
{
    public UserByUsernameSpecificaton(string username)
    {
        Criteria = user => EF.Functions
            .Collate(
                user.UserName,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(username);
    }
}

