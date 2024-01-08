using Domain.Errors.Base;

namespace Domain.Errors;

/// <summary>
///     Represent pre-defined domain error
///     including code and description.
/// </summary>
public static class DomainError
{
    /// <summary>
    ///     Represent error of role domain.
    /// </summary>
    public static class Role
    {
        /// <summary>
        ///     Prefix of error code.
        /// </summary>
        private const string RoleErrorCodePrefix = "Role";

        /// <summary>
        ///     Invalid role name error.
        /// </summary>
        public static readonly Error InvalidRoleName = new(
            code: $"{RoleErrorCodePrefix}.InvalidRoleName",
            description: "Role name is invalid.");
    }

    /// <summary>
    ///     Represent error of user domain.
    /// </summary>
    public static class User
    {
        /// <summary>
        ///    Prefix of error code.
        /// </summary>
        private const string UserErrorCodePrefix = "User";

        /// <summary>
        ///     Invalid username error.
        /// </summary>
        public static readonly Error InvalidUsername = new(
            code: $"{UserErrorCodePrefix}.InvalidUsername",
            description: "Username is invalid.");

        /// <summary>
        ///     Invalid user password error.
        /// </summary>
        public static readonly Error InvalidCreatorId = new(
            code: $"{UserErrorCodePrefix}.InvalidCreatorId",
            description: "Creator id is invalid.");

        /// <summary>
        ///     Invalid user email error.
        /// </summary>
        public static readonly Error InvalidEmail = new(
            code: $"{UserErrorCodePrefix}.InvalidEmail",
            description: "Email is invalid.");
    }

    /// <summary>
    ///     Represent error of refresh token domain.
    /// </summary>
    public static class RefreshToken
    {
        /// <summary>
        ///    Prefix of error code.
        /// </summary>
        private const string RefreshTokenErrorCodePrefix = "RefreshToken";

        /// <summary>
        ///     Invalid user id error.
        /// </summary>
        public static readonly Error InvalidUserId = new(
            code: $"{RefreshTokenErrorCodePrefix}.InvalidUserId",
            description: "User id is invalid.");

        /// <summary>
        ///     Invalid access token id error.
        /// </summary>
        public static readonly Error InvalidAccessTokenId = new(
            code: $"{RefreshTokenErrorCodePrefix}.InvalidAccessTokenId",
            description: "Access Token Id is invalid.");

        /// <summary>
        ///     Invalid creator id error.
        /// </summary>
        public static readonly Error InvalidCreatorId = new(
            code: $"{RefreshTokenErrorCodePrefix}.InvalidCreatorId",
            description: "Creator id is invalid.");
    }
}
