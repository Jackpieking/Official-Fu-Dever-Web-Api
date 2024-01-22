namespace WebApi.Options.RateLimiter.FixedWindow;

internal sealed class FixedWindowRateLimiterOption
{
    public RemoteIpAddressOption RemoteIpAddress { get; set; } = new();

    internal sealed class RemoteIpAddressOption
    {
        public int PermitLimit { get; set; }

        public int QueueProcessingOrder { get; set; }

        public int QueueLimit { get; set; }

        public int Window { get; set; }

        public bool AutoReplenishment { get; set; }
    }
}
