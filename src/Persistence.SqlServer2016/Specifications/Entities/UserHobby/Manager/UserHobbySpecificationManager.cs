using Domain.Specifications.Entities.UserHobby;
using Domain.Specifications.Entities.UserHobby.Manager;
using System;

namespace Persistence.SqlServer2016.Specifications.Entities.UserHobby.Manager;

/// <summary>
///     Represent implementation of user hobby specification manager.
/// </summary>
internal sealed class UserHobbySpecificationManager : IUserHobbySpecificationManager
{
    // Backing fields.
    private ISelectFieldsFromUserHobbySpecification _selectFieldsFromUserHobbySpecification;

    public ISelectFieldsFromUserHobbySpecification SelectFieldsFromUserHobbySpecification
    {
        get
        {
            _selectFieldsFromUserHobbySpecification ??= new SelectFieldsFromUserHobbySpecification();

            return _selectFieldsFromUserHobbySpecification;
        }
    }

    public IUserHobbyByUserIdSpecification UserHobbyByUserIdSpecification(Guid userId)
    {
        return new UserHobbyByUserIdSpecification(userId: userId);
    }
}
