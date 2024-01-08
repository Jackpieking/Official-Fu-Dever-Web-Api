namespace FuDeverWebApi.DataAccess.Specifications.Entites.UserJoiningStatus.Manager.Contratcs;

public interface IUserJoiningStatusSpecificationManager
{
    NoTrackingOnUserJoiningStatusSpecification NoTrackingOnUserJoiningStatusSpecification { get; }

    SelectFieldsFromUserJoiningStatusSpecification SelectFieldsFromUserJoiningStatusSpecification { get; }

    UserJoiningStatusByNameSpecification UserJoiningStatusByNameSpecification(string userJoiningStatusName);
}
