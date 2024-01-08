using System;

namespace FuDeverWebApi.DataAccess.Specifications.Entites.User.Manager.Contratcs;

public interface IUserSpecificationManager
{
    IsUserNotSoftRemovedSpecification IsUserNotSoftRemovedSpecification { get; }

    IsUserSoftRemovedSpecification IsUserSoftRemovedSpecification { get; }

    NoTrackingOnUserSpecification NoTrackingOnUserSpecification { get; }

    SplitQueryOnUserSpecification SplitQueryOnUserSpecification { get; }

    SelectFieldsFromUserSpecification SelectFieldsFromUserSpecification { get; }

    UserByEmailSpecification UserByEmailSpecification(string email);

    UserByPhoneNumberSpecification UserByPhoneNumberSpecification(string phoneNumber);

    UserByIdSpecification UserByIdSpecification(Guid userId);

    UserNotByIdSpecification UserNotByIdSpecification(Guid userId);

    UserByUsernameSpecificaton UserByUsernameSpecificaton(string username);

    UserByPositionIdSpecification UserByPositionIdSpecification(Guid positionId);

    UserByMajorIdSpecification UserByMajorIdSpecification(Guid majorId);

    UserByDepartmentIdSpecification UserByDepartmentIdSpecification(Guid departmentId);
}
