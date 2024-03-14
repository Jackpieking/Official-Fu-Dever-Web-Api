namespace FuDever.Configuration.Infrastructure.Mail.GoogleGmail;

public sealed class GoogleGmailSendingOption
{
    public string Mail { get; set; }

    public string DisplayName { get; set; }

    public string Password { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string WebUrl { get; set; }
}
