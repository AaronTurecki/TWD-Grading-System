using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using twd_project.ViewModels;

namespace twd_project.BusinessLogic
{
    public class EmailHelper
    {
        public const string SUCCESS = "Success";

        public string SendEmail(Email message)
        {
            const string SMTP_USER = "michael@twd.whiterabbitdev.com";
            const string SMTP_PWD = "bcitssd";
            const bool USE_HTML = true;

            const string SMTP_SERVER = "mail.twd.whiterabbitdev.com.BROWN.mysitehosted.com";
            try
            {
                MailMessage mailMsg = new MailMessage(message.Sender, message.Recipient);
                mailMsg.Subject = message.Subject;
                mailMsg.Body = message.Body;
                mailMsg.IsBodyHtml = USE_HTML;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 25;
                smtp.Host = SMTP_SERVER;
                smtp.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PWD);
                smtp.Send(mailMsg);
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            return SUCCESS;
        }

        public void SendConfirmationRequest(string user, string recipient, string confirmToken)
        {
            const string FROM = "michael@login.whiterabbitdev.com";
            const string SUBJECT = "Account Confirmation";
            string confirmLink = "http://www.twd.whiterabbitdev.com/Home/Verify?userName=" + user + "&confirmToken=" + confirmToken;
            string message = "Thank you for creating an account with us. To confirm your identity please click this link: " + confirmLink;


            Email confirmation = new Email();
            confirmation.Recipient = recipient;
            confirmation.Sender = FROM;
            confirmation.Subject = SUBJECT;
            confirmation.Body = message;

            string result = SendEmail(confirmation);

        }

        public void SendResetRequest(string recipient, string token)
        {
            const string FROM = "michael@twd.whiterabbitdev.com";
            const string SUBJECT = "Reset Password Request";
            string confirmLink = "http://www.twd.whiterabbitdev.com/Home/ChangePassword?token=" + token;
            string message = "We have received your request to reset your password. To do so, please click the following link within the next 40 minutes: " + confirmLink;


            Email confirmation = new Email();
            confirmation.Recipient = recipient;
            confirmation.Sender = FROM;
            confirmation.Subject = SUBJECT;
            confirmation.Body = message;

            string result = SendEmail(confirmation);

        }

        private string SendLocalMessage(Email message)
        {
            try
            {
                MailMessage msg = new MailMessage(message.Sender, message.Recipient,
                                                  message.Subject, message.Body);
                const string SMTP_SERVER = "localhost";
                SmtpClient emailClient = new SmtpClient(SMTP_SERVER);
                emailClient.Send(msg);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return SUCCESS;
        }

        // if local use smtp4dev otherwise use arvixe
        public string SendMessage(Email message)
        {
            string response = SUCCESS;
            if (HttpContext.Current.Request.IsLocal)
                response = SendLocalMessage(message);
            else
                response = SendEmail(message);
            return response;
        }

        public string SendStudentEmail(string userName, string subject, string body) {
            string message = "";

            if (userName == "All") {
                message = "Email successfully sent to all students";
            } else {
                message = "Email successfully sent to " + userName;
            }

            return message;
        }
    }
}