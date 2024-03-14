namespace FuDever.Configuration.Infrastructure.Mail.GoogleGmail;

public sealed class GoogleGmailSmtpServerOption
{
    public string Sender { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }
}
