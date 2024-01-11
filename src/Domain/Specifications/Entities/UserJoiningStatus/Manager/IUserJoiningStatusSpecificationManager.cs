namespace Domain.Specifications.Entities.UserJoiningStatus.Manager;

/// <summary>
///     Represent user joining status specification manager.
/// </summary>
public interface IUserJoiningStatusSpecificationManager
{
    /// <summary>
    ///     User joining status as no tracking specification.
    /// </summary>
    IUserJoiningStatusAsNoTrackingSpecification UserJoiningStatusAsNoTrackingSpecification { get; }

    /// <summary>
    ///     Select field from "UserJoiningStatuses" table specification.
    /// </summary>
    ISelectFieldsFromUserJoiningStatusSpecification SelectFieldsFromUserJoiningStatusSpecification { get; }

    /// <summary>
    ///     User joining status by user joining status type specification.
    /// </summary>
    /// <param name="userJoiningStatusType">
    ///     User joining status type for finding user joining status.
    /// </param>
    /// <returns>
    ///     Specification.
    /// </returns>
    IUserJoiningStatusByTypeSpecification UserJoiningStatusByTypeSpecification(string userJoiningStatusType);
}
