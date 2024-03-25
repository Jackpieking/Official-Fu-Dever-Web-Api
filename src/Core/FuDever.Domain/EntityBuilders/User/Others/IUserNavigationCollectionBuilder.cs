using FuDever.Domain.EntityBuilders.Others;
using System.Collections.Generic;

namespace FuDever.Domain.EntityBuilders.User.Others;

/// <summary>
///     Interface for user navigation collection builder.
/// </summary>
public interface IUserNavigationCollectionBuilder<TBuilder> :
    IBaseEntityHandler<Entities.User>
        where TBuilder : IBaseUserBuilder
{
    /// <summary>
    ///     Set user skills.
    /// </summary>
    /// <param name="userSkills">
    ///     User skills.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserSkills(IEnumerable<Entities.UserSkill> userSkills);

    /// <summary>
    ///     Set user blogs.
    /// </summary>
    /// <param name="userBlogs">
    ///     User blogs.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithBlogs(IEnumerable<Entities.Blog> userBlogs);

    /// <summary>
    ///     Set user hobbies.
    /// </summary>
    /// <param name="userHobbies">
    ///     User hobbies.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserHobbies(IEnumerable<Entities.UserHobby> userHobbies);

    /// <summary>
    ///     Set user projects.
    /// </summary>
    /// <param name="userProjects">
    ///     User projects.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithProjects(IEnumerable<Entities.Project> userProjects);

    /// <summary>
    ///     Set user blog comments.
    /// </summary>
    /// <param name="userBlogComments">
    ///     User blog comments.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithBlogComments(IEnumerable<Entities.BlogComment> userBlogComments);

    /// <summary>
    ///     Set user platforms.
    /// </summary>
    /// <param name="userPlatforms">
    ///     User platforms.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserPlatforms(IEnumerable<Entities.UserPlatform> userPlatforms);

    /// <summary>
    ///     Set user refresh tokens.
    /// </summary>
    /// <param name="userRefreshTokens">
    ///     User refresh tokens.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithRefreshTokens(IEnumerable<Entities.RefreshToken> userRefreshTokens);

    /// <summary>
    ///     Set user roles.
    /// </summary>
    /// <param name="userRoles">
    ///     User roles.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserRoles(IEnumerable<Entities.UserRole> userRoles);

    /// <summary>
    ///     Set user claims.
    /// </summary>
    /// <param name="userClaims">
    ///     User claims.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserClaims(IEnumerable<Entities.UserClaim> userClaims);

    /// <summary>
    ///     Set user logins.
    /// </summary>
    /// <param name="userLogins">
    ///     User logins.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserLogins(IEnumerable<Entities.UserLogin> userLogins);

    /// <summary>
    ///     Set user tokens.
    /// </summary>
    /// <param name="userTokens">
    ///     User tokens.
    /// </param>
    /// <returns>
    ///     Itself.
    /// </returns>
    TBuilder WithUserTokens(IEnumerable<Entities.UserToken> userTokens);
}
