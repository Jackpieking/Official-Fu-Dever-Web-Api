namespace WebApi.Options.Authorization;

public sealed class AuthorizationOption
{
    public CommonOption Common { get; set; } = new();

    public PolicyOption Policy { get; set; } = new();

    public sealed class CommonOption
    {
        public bool InvokeHandlersAfterFailure { get; set; }
    }

    public sealed class PolicyOption
    {
        public DefaultOption Default { get; set; } = new();

        public AdminRoleRequireOption AdminRoleRequire { get; set; } = new();

        public RefreshAccessTokenRequireOption RefreshAccessTokenRequire { get; set; } = new();

        public sealed class DefaultOption
        {
            public string[] AuthenticationSchemes { get; set; }
        }

        public sealed class AdminRoleRequireOption
        {
            public string[] AuthenticationSchemes { get; set; }
        }

        public sealed class RefreshAccessTokenRequireOption
        {
            public string[] AuthenticationSchemes { get; set; }
        }
    }
}
