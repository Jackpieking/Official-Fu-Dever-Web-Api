using FuDever.Domain.EntityBuilders.UserJoiningStatus;
using FuDever.Domain.Specifications.Base;
using FuDever.Domain.Specifications.Entities.UserJoiningStatus;

namespace FuDever.PostgresSql.Specifications.Entities.UserJoiningStatus;

internal sealed class SelectFieldsFromUserJoiningStatusSpecification :
    BaseSpecification<Domain.Entities.UserJoiningStatus>,
    ISelectFieldsFromUserJoiningStatusSpecification
{
    public ISelectFieldsFromUserJoiningStatusSpecification Ver1()
    {
        UserJoiningStatusForDatabaseRetrievingBuilder builder = new();

        SelectExpression = userJoiningStatus => builder
            .WithId(userJoiningStatus.Id)
            .Complete();

        return this;
    }
}
