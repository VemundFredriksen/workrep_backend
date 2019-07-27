using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workrep.Backend.DatabaseIntegration.Models;

namespace Workrep.Backend.API.Models
{
    public class EmailService
    {
        public string Salt { get; set; }

        private SendGridClient _sendGridClient;

        public EmailService(SendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }
        
        //TODO extract the mail into a document
        public async Task SendEmailConfirmationMail(User user, string ticket)
        {
            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress("workrep@noreply.no", "WorkRep Team"));
            message.AddTo(new EmailAddress(user.Email, user.Name));

            message.SetSubject("Email Verification of WorkRep account");

            string emailContent = "<h2>Email Verification</h2></br>" +
                "<p>Hi! A WorkRep account is registered at your email address.</br> If you recently " +
                "registered a WorkRep account please click the following link: " +
                "<a href=" + "workrep.azurewebsites.net"
                + "/api/v2/user/verifyemail?email=" + user.Email + "&key=" + ticket + ">" + ticket + "</a></p>" +
                "</br></br><p>If you did not register a WorkRep account, please ignore this mail</p>";

            message.AddContent(MimeType.Html, emailContent);

            _sendGridClient.SendEmailAsync(message);
        }

    }
}
