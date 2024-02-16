using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserJoiningStatus;

namespace Persistence.Database.SqlServer.Specifications.Entities.UserJoiningStatus;

internal sealed class SelectFieldsFromUserJoiningStatusSpecification :
    BaseSpecification<Domain.Entities.UserJoiningStatus>,
    ISelectFieldsFromUserJoiningStatusSpecification
{
    public ISelectFieldsFromUserJoiningStatusSpecification Ver1()
    {
        SelectExpression = userJoiningStatus => Domain.Entities.UserJoiningStatus.InitVer2(userJoiningStatus.Id);

        return this;
    }
}
