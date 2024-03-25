using FuDever.Domain.EntityBuilders.Others;

namespace FuDever.Domain.EntityBuilders.User.Others;

/// <summary>
///     Interface for user navigation property builder.
/// </summary>
public interface IUserNavigationPropertyBuilder<TBuilder> :
    IBaseEntityHandler<Entities.User>
        where TBuilder : IBaseUserBuilder
{
    /// <summary>
    ///     Set user position.
    /// </summary>
    /// <param name="userPosition">
    ///     User position.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithPosition(Entities.Position userPosition);

    /// <summary>
    ///     Set user major.
    /// </summary>
    /// <param name="userMajor">
    ///     User major.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithMajor(Entities.Major userMajor);

    /// <summary>
    ///     Set user department.
    /// </summary>
    /// <param name="userDepartment">
    ///     User department.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithDepartment(Entities.Department userDepartment);

    /// <summary>
    ///     Set user joining status.
    /// </summary>
    /// <param name="userJoiningStatus">
    ///     User joining status.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserJoiningStatus(Entities.UserJoiningStatus userJoiningStatus);
}
