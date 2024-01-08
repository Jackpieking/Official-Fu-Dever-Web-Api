using FuDeverWebApi.DataAccess.Specifications.Entites.UserJoiningStatus.Manager.Contratcs;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserJoiningStatus.Manager.Implementations;

public sealed class UserJoiningStatusSpecificationManager :
    IUserJoiningStatusSpecificationManager
{
    private NoTrackingOnUserJoiningStatusSpecification _noTrackingOnUserJoiningStatusSpecification;
    private SelectFieldsFromUserJoiningStatusSpecification _selectFieldsFromUserJoiningStatusSpecification;

    public NoTrackingOnUserJoiningStatusSpecification NoTrackingOnUserJoiningStatusSpecification
    {
        get
        {
            _noTrackingOnUserJoiningStatusSpecification ??= new();

            return _noTrackingOnUserJoiningStatusSpecification;
        }
    }

    public SelectFieldsFromUserJoiningStatusSpecification SelectFieldsFromUserJoiningStatusSpecification
    {
        get
        {
            _selectFieldsFromUserJoiningStatusSpecification ??= new();

            return _selectFieldsFromUserJoiningStatusSpecification;
        }
    }

    public UserJoiningStatusByNameSpecification UserJoiningStatusByNameSpecification(
        string userJoiningStatusName)
    {
        return new(userJoiningStatusName: userJoiningStatusName);
    }
}
