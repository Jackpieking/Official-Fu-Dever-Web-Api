using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserPlatform;

namespace Persistence.RelationalDatabase.SqlServer.Specifications.Entities.UserPlatform;

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
        SelectExpression = userPlatform => Domain.Entities.UserPlatform.InitVer2(userPlatform.UserId);

        return this;
    }
}
