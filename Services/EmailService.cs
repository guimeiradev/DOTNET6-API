using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Blog.Services;

public class EmailService
{
    public bool Send(
        string toName,
        string toEmail,
        string subject,
        string body,
        string fromName = "Equipe guimeiraDev",
        string fromEmail = "guimeiradev@gmail.com")
    {
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);
        smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;

        var sendGridClient = 
            new SendGridClient("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");
        var sendGridMessage = new SendGridMessage();
        sendGridMessage.SetFrom(fromEmail, fromName);
        sendGridMessage.AddTo(toEmail,toName);
//The Template Id will be something like this - d-9416e4bc396e4e7fbb658900102abaa2
        sendGridMessage.SetTemplateId("d-cf273202ea824f9384f71a12ab396a43");
//Here is the Place holder values you need to replace.
        sendGridMessage.SetTemplateData(new
        {
            Name = toName,
        });
        try
        {
            sendGridClient.SendEmailAsync(sendGridMessage);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}