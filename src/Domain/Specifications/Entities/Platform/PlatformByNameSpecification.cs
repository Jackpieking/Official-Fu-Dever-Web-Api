using FuDeverWebApi.DataAccess.Data.Entities;
using FuDeverWebApi.DataAccess.Specifications.Generic.Implementations;
using FuDeverWebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.Platform;

public sealed class PlatformByNameSpecification :
    GenericSpecification<PlatformEntity>
{
    public PlatformByNameSpecification(
        string platformName,
        bool isCaseSensitive = false)
    {
        if (!isCaseSensitive)
        {
            Criteria = platform => platform.Name.Equals(platformName);

            return;
        }

        Criteria = platform => EF.Functions
            .Collate(
                platform.Name,
                CustomConstants.SqlCollation.SQL_LATIN1_GENERAL_CP1_CS_AS)
            .Equals(platformName);
    }
}
