using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Isdg.Core;
using Isdg.Core.Data;

namespace Isdg.Services.Messages
{
    /// <summary>
    /// Email sender
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public void SendEmailOnCreate(IEnumerable<string> emails, string entityName, string detailsUrl, string username = null)
        {
            var emailAccount = Settings.EmailAccount;
            var fromEmail = emailAccount.Email;
            var fromName = emailAccount.DisplayName;
            var subject = String.Format("New {0} has been created", entityName);
            var body = String.Format("New {0} has been created{2}. Click <a href=\"{1}\">here</a> for more details.", entityName, detailsUrl, !String.IsNullOrEmpty(username) ? " by user " + username : "");
            var fromAddress = new MailAddress(fromEmail, fromName);
            foreach (var email in emails)
            {
                SendEmail(emailAccount, subject, body, fromAddress, new MailAddress(email, ""));
            }
        }

        public void SendConfirmationEmail(string email, string callbackUrl)
        {
            var body = "<p>Confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.</p>" +
                "<p>You've received this e-mail because you've been registered on the site http://isdg-site.org/. If it isn't you, just ignore the letter.</p>";
            SendEmail("Confirm your account", body, email);
        }
        
        public void SendEmail(string subject, string body, string toAddress, string toName = "",
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            var emailAccount = Settings.EmailAccount;
            var fromAddress = emailAccount.Email;
            var fromName = emailAccount.DisplayName;
            SendEmail(emailAccount, subject, body,
                new MailAddress(fromAddress, fromName), new MailAddress(toAddress, toName),
                bcc, cc, attachmentFilePath, attachmentFileName);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="fromAddress">From address</param>
        /// <param name="fromName">From display name</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        public void SendEmail(EmailAccount emailAccount, string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            SendEmail(emailAccount, subject, body, 
                new MailAddress(fromAddress, fromName), new MailAddress(toAddress, toName),
                bcc, cc, attachmentFilePath, attachmentFileName);
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailAccount">Email account to use</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="from">From address</param>
        /// <param name="to">To address</param>
        /// <param name="bcc">BCC addresses list</param>
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        public void SendEmail(EmailAccount emailAccount, string subject, string body,
            MailAddress from, MailAddress to,
            IEnumerable<string> bcc = null, IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            var message = new MailMessage();
            message.From = from;
            message.To.Add(to);
            if (bcc != null)
            {
                foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
                {
                    message.Bcc.Add(address.Trim());
                }
            }
            if (cc != null)
            {
                foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
                {
                    message.CC.Add(address.Trim());
                }
            }
                 
            message.Subject = subject;
            message.Body = MakePretty(body);
            message.IsBodyHtml = true;

            //create  the file attachment for this e-mail message
            if (!String.IsNullOrEmpty(attachmentFilePath) &&
                File.Exists(attachmentFilePath))
            {
                var attachment = new Attachment(attachmentFilePath);
                attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentFilePath);
                attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentFilePath);
                attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentFilePath);
                if (!String.IsNullOrEmpty(attachmentFileName))
                {
                    attachment.Name = attachmentFileName;
                }
                message.Attachments.Add(attachment);
            }

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                smtpClient.Host = emailAccount.Host;
                smtpClient.Port = emailAccount.Port;
                smtpClient.EnableSsl = emailAccount.EnableSsl;
                if (emailAccount.UseDefaultCredentials)
                    smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                else
                    smtpClient.Credentials = new NetworkCredential(emailAccount.Username, emailAccount.Password);
                smtpClient.Send(message);
            }
        }

        private string MakePretty(string body)
        {
            var header = "<div></div><div style=\"height:20px;margin-top:5px;margin-bottom:5px;background-color:#793962\"></div><div><div style=\"font-style:italic;font-weight:bold;font-size:14px;width: 200px;\">International Society of Dynamic Games</div><div style=\"font-weight:bold;font-size:68px;margin-left:35px;\">ISDG</div></div>";
            var footer = "<div style=\"height:20px;margin-top:5px;margin-bottom:5px;background-color:#793962;text-align:center;\"><a style=\"color:#fff;\" href=\"http://isdg-site.org/\">ISDG</a></div>";
            return String.Format("{0}<p>{1}</p>{2}", header, body, footer);
        }
    }
}
