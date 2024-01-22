using Domain.Specifications.Base;
using Domain.Specifications.Entities.UserJoiningStatus;

namespace Persistence.SqlServer2016.Specifications.Entities.UserJoiningStatus;

internal sealed class SelectFieldsFromUserJoiningStatusSpecification :
    BaseSpecification<Domain.Entities.UserJoiningStatus>,
    ISelectFieldsFromUserJoiningStatusSpecification
{
    public ISelectFieldsFromUserJoiningStatusSpecification Ver1()
    {
        SelectExpression = userJoiningStatus => Domain.Entities.UserJoiningStatus.Init(userJoiningStatus.Id);

        return this;
    }
}
