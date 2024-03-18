using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserJoiningStatus;

namespace FuDever.PostgresSql.Specifications.Entities.UserJoiningStatus;

internal sealed class SelectFieldsFromUserJoiningStatusSpecification :
    BaseSpecification<Domain.Entities.UserJoiningStatus>,
    ISelectFieldsFromUserJoiningStatusSpecification
{
    public ISelectFieldsFromUserJoiningStatusSpecification Ver1()
    {
        SelectExpression = userJoiningStatus => Domain.Entities.UserJoiningStatus.InitFromDatabaseVer1(
            userJoiningStatus.Id);

        return this;
    }
}
