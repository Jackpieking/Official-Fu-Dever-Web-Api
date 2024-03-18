using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserPlatform;

namespace FuDever.PostgresSql.Specifications.Entities.UserPlatform;

/// <summary>
///     Represent implementation of select fields from the "UserPlatforms"
///     table specification.
/// </summary>
internal sealed class SelectFieldsFromUserPlatformSpecification :
    BaseSpecification<Domain.Entities.UserPlatform>,
    ISelectFieldsFromUserPlatformSpecification
{
    public ISelectFieldsFromUserPlatformSpecification Ver1()
    {
        SelectExpression = userPlatform => Domain.Entities.UserPlatform.InitFromDatabaseVer2(
            userPlatform.UserId);

        return this;
    }
}
