using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserPlatform;

namespace Persistence.SqlServer2016.Specifications.Entities.UserPlatform;

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
        SelectExpression = userPlatform => Domain.Entities.UserPlatform.Init(userPlatform.UserId);

        return this;
    }
}
