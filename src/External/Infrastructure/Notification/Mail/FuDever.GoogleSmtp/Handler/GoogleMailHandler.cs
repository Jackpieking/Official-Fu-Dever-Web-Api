using FuDever.Application.Interfaces.Mail;
using FuDever.Application.Models;
using FuDever.Configuration.Infrastructure.Mail.GoogleGmail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FuDever.GoogleSmtp.Handler;

/// <summary>
///     Google mail handler that implement
///     mail handler interface.
/// </summary>
internal sealed class GoogleMailHandler : IMailHandler
{
    private readonly GoogleGmailSmtpServerOption _googleGmailSmtpServerOption;
    private readonly GoogleGmailSendingOption _googleGmailSendingOption;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GoogleMailHandler(
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        _googleGmailSmtpServerOption = configuration
            .GetRequiredSection(key: "SmtpServerCommunication")
            .GetRequiredSection(key: "GoogleGmail")
            .Get<GoogleGmailSmtpServerOption>();

        _googleGmailSendingOption = configuration
            .GetRequiredSection("MailSending")
            .GetRequiredSection("GoogleGmail")
            .Get<GoogleGmailSendingOption>();

        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> IsRealAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(value: email))
        {
            return false;
        }

        const string CRLF = "\r\n";
        byte[] dataBuffer;
        Memory<byte> dataBufferAsMemory;
        bool isEmailFound = true;

        using TcpClient tClient = new(
            hostname: _googleGmailSmtpServerOption.Host,
            port: _googleGmailSmtpServerOption.Port);
        await using var netStream = tClient.GetStream();
        using StreamReader reader = new(stream: netStream);

        await reader.ReadLineAsync();

        // Init the connection to smtp server.
        dataBuffer = Encoding.ASCII.GetBytes(s: $"HELO FuDeverWebApi{CRLF}");

        dataBufferAsMemory = dataBuffer.AsMemory(
            start: default,
            length: dataBuffer.Length);

        // Send message to server
        await netStream.WriteAsync(buffer: dataBufferAsMemory);

        await reader.ReadLineAsync();

        // Telling the server who is the sender.
        dataBuffer = Encoding.ASCII.GetBytes(
            s: $"MAIL FROM:<{_googleGmailSmtpServerOption.Sender}>{CRLF}");

        dataBufferAsMemory = dataBuffer.AsMemory(
            start: default,
            length: dataBuffer.Length);

        // Send message to server
        await netStream.WriteAsync(buffer: dataBufferAsMemory);

        await reader.ReadLineAsync();

        // Validating the receiver through server.
        dataBuffer = Encoding.ASCII.GetBytes(s: $"RCPT TO:<{email}>{CRLF}");

        dataBufferAsMemory = dataBuffer.AsMemory(
            start: default,
            length: dataBuffer.Length);

        // Send message to server
        await netStream.WriteAsync(buffer: dataBufferAsMemory);

        // Get the message from the server.
        var message = await reader.ReadLineAsync();

        // Extract and get the message status code.
        var messageStatusCode = int.Parse(s: message[..3]);

        // Email is not found.
        if (messageStatusCode == 550)
        {
            isEmailFound = false;
        }

        // Close connection
        dataBuffer = Encoding.ASCII.GetBytes(s: $"QUIT{CRLF}");

        dataBufferAsMemory = dataBuffer.AsMemory(
            start: default,
            length: dataBuffer.Length);

        // Send message to server
        await netStream.WriteAsync(buffer: dataBufferAsMemory);

        return isEmailFound;
    }

    public async Task<MailContentModel> GetUserAccountConfirmationMailContentAsync(
        string to,
        string subject,
        string mainVerifyLink,
        string alternativeVerifyLink,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(value: to) ||
            string.IsNullOrWhiteSpace(value: subject) ||
            string.IsNullOrWhiteSpace(value: mainVerifyLink) ||
            string.IsNullOrWhiteSpace(value: alternativeVerifyLink) ||
            cancellationToken == CancellationToken.None)
        {
            return null;
        }

        //Construct html template path.
        var MailTemplateFilePath = Path.Combine(
            path1: "CreateUserAccount",
            path2: "AskUserToConfirmedAccountMailTemplate.html");

        var htmlTemplatePath = Path.Combine(
            path1: _webHostEnvironment.WebRootPath,
            path2: MailTemplateFilePath);

        //Get the html template from file.
        var htmlTemplate = await File.ReadAllTextAsync(
            path: htmlTemplatePath,
            cancellationToken: cancellationToken);

        StringBuilder mailBody = new(value: htmlTemplate);

        mailBody
            .Replace(
                oldValue: "{verify-link1}",
                newValue: $"{_googleGmailSendingOption.WebUrl}{mainVerifyLink}")
            .Replace(
                oldValue: "{verify-link2}",
                newValue: $"{_googleGmailSendingOption.WebUrl}{alternativeVerifyLink}");

        MailContentModel mailContent = new()
        {
            To = to,
            Subject = subject,
            Body = mailBody.ToString()
        };

        return mailContent;
    }

    public async Task SendAsync(
        MailContentModel mailContent,
        CancellationToken cancellationToken)
    {
        if (Equals(objA: mailContent, objB: null))
        {
            return;
        }

        //Init an email for sending.
        MimeMessage email = new()
        {
            Sender = new(
                name: _googleGmailSendingOption.DisplayName,
                address: _googleGmailSendingOption.Mail)
        };

        //Add the "from" section.
        email.From.Add(
            address: new MailboxAddress(
                name: _googleGmailSendingOption.DisplayName,
                address: _googleGmailSendingOption.Mail));

        //Add the "to" section.
        email.To.Add(
            address: MailboxAddress.Parse(
                text: mailContent.To));

        //Add the "subject" section.
        email.Subject = mailContent.Subject;

        //Add the "body" section.
        BodyBuilder builder = new()
        {
            HtmlBody = mailContent.Body
        };

        email.Body = builder.ToMessageBody();
        using SmtpClient smtp = new();

        await smtp.ConnectAsync(
            host: _googleGmailSendingOption.Host,
            port: _googleGmailSendingOption.Port,
            options: SecureSocketOptions.StartTlsWhenAvailable,
            cancellationToken: cancellationToken);

        await smtp.AuthenticateAsync(
            userName: _googleGmailSendingOption.Mail,
            password: _googleGmailSendingOption.Password,
            cancellationToken: cancellationToken);

        await smtp.SendAsync(
            message: email,
            cancellationToken: cancellationToken);

        await smtp.DisconnectAsync(
            quit: true,
            cancellationToken: cancellationToken);
    }
}
