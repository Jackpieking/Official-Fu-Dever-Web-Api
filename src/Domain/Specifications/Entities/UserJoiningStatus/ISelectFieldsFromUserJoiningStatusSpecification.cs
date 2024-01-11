using Domain.Specifications.Base;

namespace Domain.Specifications.Entities.UserJoiningStatus;

/// <summary>
///     Represent select fields from the "UserJoiningStatuses" table specification.
/// </summary>
public interface ISelectFieldsFromUserJoiningStatusSpecification : IBaseSpecification<Domain.Entities.UserJoiningStatus>
{
    ISelectFieldsFromUserJoiningStatusSpecification Ver1();
}
