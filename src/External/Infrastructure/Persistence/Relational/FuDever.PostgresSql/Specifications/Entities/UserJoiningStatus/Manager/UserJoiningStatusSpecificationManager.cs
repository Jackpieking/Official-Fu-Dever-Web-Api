using FuDever.Domain.Specifications.Entities.UserJoiningStatus;
using FuDever.Domain.Specifications.Entities.UserJoiningStatus.Manager;

namespace FuDever.PostgresSql.Specifications.Entities.UserJoiningStatus.Manager;

/// <summary>
///     Represent implementation of user joining status specification manager.
/// </summary>
internal sealed class UserJoiningStatusSpecificationManager : IUserJoiningStatusSpecificationManager
{
    // Backing fields.
    private IUserJoiningStatusAsNoTrackingSpecification _userJoiningStatusAsNoTrackingSpecification;
    private ISelectFieldsFromUserJoiningStatusSpecification _selectFieldsFromUserJoiningStatusSpecification;

    public IUserJoiningStatusAsNoTrackingSpecification UserJoiningStatusAsNoTrackingSpecification
    {
        get
        {
            _userJoiningStatusAsNoTrackingSpecification ??= new UserJoiningStatusAsNoTrackingSpecification();

            return _userJoiningStatusAsNoTrackingSpecification;
        }
    }

    public ISelectFieldsFromUserJoiningStatusSpecification SelectFieldsFromUserJoiningStatusSpecification
    {
        get
        {
            _selectFieldsFromUserJoiningStatusSpecification ??= new SelectFieldsFromUserJoiningStatusSpecification();

            return _selectFieldsFromUserJoiningStatusSpecification;
        }
    }

    public IUserJoiningStatusByTypeSpecification UserJoiningStatusByTypeSpecification(string userJoiningStatusType)
    {
        return new UserJoiningStatusByTypeSpecification(userJoiningStatusType: userJoiningStatusType);
    }
}
