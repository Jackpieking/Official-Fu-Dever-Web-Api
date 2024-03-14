namespace FuDever.Application.Models;

/// <summary>
///     Represent the mail content model.
/// </summary>
public sealed class MailContentModel
{
    /// <summary>
    ///     Send the email to whom.
    /// </summary>
    public string To { get; set; }

    /// <summary>
    ///     What the subject of mail.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    ///     What is the body of mail.
    /// </summary>
    public string Body { get; set; }
}
