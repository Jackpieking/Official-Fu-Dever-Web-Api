namespace WebApi.Options.Authorization;

internal sealed class AuthorizationOption
{
    public CommonOption Common { get; set; } = new();

    public PolicyOption Policy { get; set; } = new();

    internal sealed class CommonOption
    {
        public bool InvokeHandlersAfterFailure { get; set; }
    }

    internal sealed class PolicyOption
    {
        public DefaultOption Default { get; set; } = new();

        public AdminRoleRequireOption AdminRoleRequire { get; set; } = new();

        public RefreshAccessTokenRequireOption RefreshAccessTokenRequire { get; set; } = new();

        internal sealed class DefaultOption
        {
            public string[] AuthenticationSchemes { get; set; } = [];
        }

        internal sealed class AdminRoleRequireOption
        {
            public string[] AuthenticationSchemes { get; set; } = [];
        }

        internal sealed class RefreshAccessTokenRequireOption
        {
            public string[] AuthenticationSchemes { get; set; } = [];
        }
    }
}
