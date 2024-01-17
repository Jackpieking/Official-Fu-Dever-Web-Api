namespace Persistence.SqlServer2016.Options.AspNetCoreIdentity;

/// <summary>
///     Represent section of "AspNetCoreIdentity" section in
///     appsetting.json configuration file.
/// </summary>
public sealed class AspNetCoreIdentityOption
{
    /// <summary>
    ///     Reference to password section.
    /// </summary>
    public PasswordOption Password { get; set; } = new();

    /// <summary>
    ///     Reference to lockout section.
    /// </summary>
    public LockoutOption Lockout { get; set; } = new();

    /// <summary>
    ///     Reference to user section.
    /// </summary>
    public UserOption User { get; set; } = new();

    /// <summary>
    ///     Reference to sign in section.
    /// </summary>
    public SignInOption SignIn { get; set; } = new();

    /// <summary>
    ///     Password section.
    /// </summary>
    public sealed class PasswordOption
    {
        /// <summary>
        ///     Password require digit.
        /// </summary>
        public bool RequireDigit { get; set; }

        /// <summary>
        ///     Password require lowercase.
        /// </summary>
        public bool RequireLowercase { get; set; }

        /// <summary>
        ///     Password require non alpha numeric.
        /// </summary>
        public bool RequireNonAlphanumeric { get; set; }

        /// <summary>
        ///     Password require uppercase.
        /// </summary>
        public bool RequireUppercase { get; set; }

        /// <summary>
        ///     Password require length.
        /// </summary>
        public int RequiredLength { get; set; }

        /// <summary>
        ///     Password require unique chars.
        /// </summary>
        public int RequiredUniqueChars { get; set; }
    }

    /// <summary>
    ///     Lockout section.
    /// </summary>
    public sealed class LockoutOption
    {
        /// <summary>
        ///     Default lock out time.
        /// </summary>
        public int DefaultLockoutTimeSpanInSecond { get; set; }

        /// <summary>
        ///     Max failed access attempts before locking out.
        /// </summary>
        public int MaxFailedAccessAttempts { get; set; }

        /// <summary>
        ///     Lockout for new user.
        /// </summary>
        public bool AllowedForNewUsers { get; set; }
    }

    /// <summary>
    ///     User section.
    /// </summary>
    public sealed class UserOption
    {
        /// <summary>
        ///     List of characters that username can have.
        /// </summary>
        public string AllowedUserNameCharacters { get; set; }

        /// <summary>
        ///     Email must be unique.
        /// </summary>
        public bool RequireUniqueEmail { get; set; }
    }

    /// <summary>
    ///     Sign in section.
    /// </summary>
    public sealed class SignInOption
    {
        /// <summary>
        ///     User email need confirming or not.
        /// </summary>
        public bool RequireConfirmedEmail { get; set; }

        /// <summary>
        ///     User phone number need confirming or not.
        /// </summary>
        public bool RequireConfirmedPhoneNumber { get; set; }
    }
}
