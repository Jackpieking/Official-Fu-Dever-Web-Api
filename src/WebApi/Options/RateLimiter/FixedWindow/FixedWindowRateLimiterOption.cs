namespace WebApi.Options.RateLimiter.FixedWindow;

internal sealed class FixedWindowRateLimiterOption
{
    internal RemoteIpAddressOption RemoteIpAddress { get; set; } = new();

    internal sealed class RemoteIpAddressOption
    {
        internal int PermitLimit { get; set; }

        internal int QueueProcessingOrder { get; set; }

        internal int QueueLimit { get; set; }

        internal int Window { get; set; }

        internal bool AutoReplenishment { get; set; }
    }
}
