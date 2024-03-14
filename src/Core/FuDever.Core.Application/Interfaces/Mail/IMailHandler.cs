using FuDever.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.Application.Interfaces.Mail;

/// <summary>
///     Represent interface of mail handler.
/// </summary>
public interface IMailHandler
{
    /// <summary>
    ///     Validate if the email is real or not.
    /// </summary>
    /// <param name="email">
    ///     User email
    /// </param>
    /// <returns>
    ///     True if email is exist. Otherwise, false.
    /// </returns>
    Task<bool> IsRealAsync(string email);

    /// <summary>
    ///     Get user account confirmation mail content.
    /// </summary>
    /// <param name="to">
    ///     Send to whom.
    /// </param>
    /// <param name="subject">
    ///     Mail subject
    /// </param>
    /// <param name="mainVerifyLink">
    ///     Main mail verification link.
    /// </param>
    /// <param name="alternativeVerifyLink">
    ///     Alternative mail verification link.
    /// </param
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Model contain receiver information.
    /// </returns>
    Task<MailContentModel> GetUserAccountConfirmationMailContentAsync(
        string to,
        string subject,
        string mainVerifyLink,
        string alternativeVerifyLink,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Sending an email to the specified user.
    /// </summary>
    /// <param name="mailContent">
    ///     A model contains all receiver information.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Empty task.
    /// </returns>
    Task SendAsync(
        MailContentModel mailContent,
        CancellationToken cancellationToken);
}
