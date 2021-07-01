using Microsoft.Extensions.Options;
using PracticeCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCore.Services
{
    public class EmailService : IEmailService
    {
        private string tempPath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfig _options;

        public EmailService(IOptions<SMTPConfig> options)
        {
            _options = options.Value;
        }
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = Placeholder("Hello {{UserName}} from Book Store",userEmailOptions.PlaceHolders);
            userEmailOptions.Body = Placeholder(GetBody("TestEmail"),userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }
        public async Task ConfirmEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = Placeholder("Hello {{UserName}}, Confirm Your Email", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = Placeholder(GetBody("ConfirmEmail"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }
        public async Task ForgotPasswordEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = Placeholder("Hello {{UserName}}, Reset Password", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = Placeholder(GetBody("ForgotPassword"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }
        private async Task SendEmail(UserEmailOptions options)
        {
            MailMessage mail = new MailMessage()
            {
                Subject = options.Subject,
                Body = options.Body,
                From = new MailAddress(_options.SenderAddress, _options.SenderDisplayName),
                IsBodyHtml = _options.IsBodyHTML
            };
            foreach (var tomail in options.ToEmails)
            {
                mail.To.Add(tomail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_options.Username, _options.Password);
            SmtpClient client = new SmtpClient()
            {
                Host = _options.Host,
                Port = _options.Port,
                EnableSsl = _options.EnableSSL,
                UseDefaultCredentials = _options.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;
            await client.SendMailAsync(mail);
        }
        private string GetBody(String TempName)
        {
            return File.ReadAllText(string.Format(tempPath, TempName));
        }
        private string Placeholder(string text,List<KeyValuePair<string,string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text)&& keyValuePairs != null)
            {
                foreach (var item in keyValuePairs)
                {
                    if (text.Contains(item.Key))
                    {
                        text = text.Replace(item.Key, item.Value);
                    }
                }
            }
            return text;
        }
        
    }
}
