//using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using MimeKit;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace PlutoHealthWeb.PlutoHelpers
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailHelper _emailHelper;
        private readonly IWebHostEnvironment _env;

        public EmailSender(IOptions<EmailHelper> emailHelper, IWebHostEnvironment env)
        {
           _emailHelper = emailHelper.Value;
           _env = env;
        }
        public async Task SendEmailAsyc(string email, string subject, string message)
        {
            //return Task.CompletedTask;
            System.Console.WriteLine("Test");
            System.Console.WriteLine("My HOST "+ _emailHelper.Server);
            try 
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_emailHelper.SenderName, _emailHelper.SenderEmail));
                mimeMessage.To.Add(new MailboxAddress(email));
                mimeMessage.Subject = subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };
                
                using (var client = new SmtpClient())
                {
                    //for demo purposes, accept all the ssl certificates (incase server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if(_env.IsDevelopment())
                    {
                        // The third parameter is useSSL (true if the client should make an SSL-wrapped
                       // connection to the server; otherwise, false).

                       await client.ConnectAsync(_emailHelper.Server, _emailHelper.Port, true);
                    }
                    else
                    {
                        await client.ConnectAsync(_emailHelper.Server);
                    }

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(_emailHelper.SenderEmail, _emailHelper.Password);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }

            }
            catch
            {
                // TODO: handle exception
                //throw new InvalidOperationException(ex.Message);
            }
        }
    }
}