namespace Persistence.SqlServer2016.Options.AspNetCoreIdentity;

/// <summary>
///     Represent section of "AspNetCoreIdentity" section in
///     appsetting.json configuration file.
/// </summary>
internal sealed class AspNetCoreIdentityOption
{
    /// <summary>
    ///     Reference to password section.
    /// </summary>
    internal PasswordOption Password { get; set; } = new();

    /// <summary>
    ///     Reference to lockout section.
    /// </summary>
    internal LockoutOption Lockout { get; set; } = new();

    /// <summary>
    ///     Reference to user section.
    /// </summary>
    internal UserOption User { get; set; } = new();

    /// <summary>
    ///     Reference to sign in section.
    /// </summary>
    internal SignInOption SignIn { get; set; } = new();

    /// <summary>
    ///     Password section.
    /// </summary>
    internal sealed class PasswordOption
    {
        /// <summary>
        ///     Password require digit.
        /// </summary>
        internal bool RequireDigit { get; set; }

        /// <summary>
        ///     Password require lowercase.
        /// </summary>
        internal bool RequireLowercase { get; set; }

        /// <summary>
        ///     Password require non alpha numeric.
        /// </summary>
        internal bool RequireNonAlphanumeric { get; set; }

        /// <summary>
        ///     Password require uppercase.
        /// </summary>
        internal bool RequireUppercase { get; set; }

        /// <summary>
        ///     Password require length.
        /// </summary>
        internal int RequiredLength { get; set; }

        /// <summary>
        ///     Password require unique chars.
        /// </summary>
        internal int RequiredUniqueChars { get; set; }
    }

    /// <summary>
    ///     Lockout section.
    /// </summary>
    internal sealed class LockoutOption
    {
        /// <summary>
        ///     Default lock out time.
        /// </summary>
        internal int DefaultLockoutTimeSpanInSecond { get; set; }

        /// <summary>
        ///     Max failed access attempts before locking out.
        /// </summary>
        internal int MaxFailedAccessAttempts { get; set; }

        /// <summary>
        ///     Lockout for new user.
        /// </summary>
        internal bool AllowedForNewUsers { get; set; }
    }

    /// <summary>
    ///     User section.
    /// </summary>
    internal sealed class UserOption
    {
        /// <summary>
        ///     List of characters that username can have.
        /// </summary>
        internal string AllowedUserNameCharacters { get; set; }

        /// <summary>
        ///     Email must be unique.
        /// </summary>
        internal bool RequireUniqueEmail { get; set; }
    }

    /// <summary>
    ///     Sign in section.
    /// </summary>
    internal sealed class SignInOption
    {
        /// <summary>
        ///     User email need confirming or not.
        /// </summary>
        internal bool RequireConfirmedEmail { get; set; }

        /// <summary>
        ///     User phone number need confirming or not.
        /// </summary>
        internal bool RequireConfirmedPhoneNumber { get; set; }
    }
}
