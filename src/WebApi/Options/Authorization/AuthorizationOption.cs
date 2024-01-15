namespace WebApi.Options.Authorization;

internal sealed class AuthorizationOption
{
    internal CommonOption Common { get; set; } = new();

    internal PolicyOption Policy { get; set; } = new();

    internal sealed class CommonOption
    {
        internal bool InvokeHandlersAfterFailure { get; set; }
    }

    internal sealed class PolicyOption
    {
        internal DefaultOption Default { get; set; } = new();

        internal AdminRoleRequireOption AdminRoleRequire { get; set; } = new();

        internal RefreshAccessTokenRequireOption RefreshAccessTokenRequire { get; set; } = new();

        internal sealed class DefaultOption
        {
            internal string[] AuthenticationSchemes { get; set; }
        }

        internal sealed class AdminRoleRequireOption
        {
            internal string[] AuthenticationSchemes { get; set; }
        }

        internal sealed class RefreshAccessTokenRequireOption
        {
            internal string[] AuthenticationSchemes { get; set; }
        }
    }
}
