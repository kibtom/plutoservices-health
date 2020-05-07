using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
//using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using MimeKit;
using MailKit;
using MailKit.Security;
using MailKit.Net.Smtp;
using PlutoHealthWeb.PlutoHelpers;
using PlutoHealthWeb.Models;

namespace PlutoHealthWeb.Pages
{
    public class ContactModel : PageModel
    {
              
        public const string COMPANY_EMAIL = "info@plutobvservices.co.uk";
        private readonly EmailHelper _emailHelper;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailSender _emailsender;

        
       [BindProperty]
        public UserFeedbackForm FeedbackForm{get; set;}


        public ContactModel(IOptions<EmailHelper> emailHelper, IWebHostEnvironment env) 
        {
            _emailHelper = emailHelper.Value;
            _env = env;
        }

        // public ContactModel(IEmailSender emailSender)
        // {
        //     _emailsender = emailSender;            
        // }

       

        public void OnGet()
        {

        }
        
        
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                 //Console.WriteLine("Testing"+ _emailHelper.SenderEmail);
                return Page();
            }  

            var userMessage = new BodyBuilder();
            var recipients = COMPANY_EMAIL + ";" + FeedbackForm.UserEmailAddress;
            var emailSubject = "Pluto BV Website Feedback";

            InternetAddressList recipientList = new InternetAddressList();
            recipientList.Add(new MailboxAddress(FeedbackForm.UserEmailAddress));
            recipientList.Add(new MailboxAddress(COMPANY_EMAIL));

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailHelper.SenderName, _emailHelper.SenderEmail));
            //mimeMessage.To.Add(new MailboxAddress(FeedbackForm.UserEmailAddress));
            mimeMessage.To.AddRange(recipientList);
            mimeMessage.Subject = emailSubject;

            StringBuilder sbEmailBody = new StringBuilder();
            try 
            {
                if(_emailHelper.IsEmailEnabled == "YES")
                {
                                        //To revisit this as this a feedback from website and not 
                    //Append the collections from the form

    userMessage.TextBody =
    $@"Dear {FeedbackForm.UserName}
    
The following details were submitted successfully. A member of our team will be in touch soon: 
    
    
    Name:     {FeedbackForm.UserName};
    Postcode: {FeedbackForm.UserPostcode} 
    Email:    {FeedbackForm.UserEmailAddress}
    Phone:    {FeedbackForm.UserPhoneNumber}
    
    Message: {FeedbackForm.UserMessage}

    
    Thanks for the feedback...
    
    Kind Regards
    PlutoBvServices Ltd";


                    mimeMessage.Body = userMessage.ToMessageBody();

                    using (var client = new SmtpClient())
                    {
                        //for demo purposes, accept all the ssl certificates (incase server supports STARTTLS)
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        
                            // The third parameter is useSSL (true if the client should make an SSL-wrapped
                        // connection to the server; otherwise, false).
                        
                        await client.ConnectAsync(_emailHelper.Server, _emailHelper.Port);                
                    
                        // Note: only needed if the SMTP server requires authentication
                        await client.AuthenticateAsync(_emailHelper.SenderEmail, _emailHelper.Password);
                        await client.SendAsync(mimeMessage);
                        await client.DisconnectAsync(true);
                    }                    

                }
                    
                else 
                {
                    System.Console.WriteLine("NOt ENABLED");
                }
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            

            ModelState.Clear();
            return RedirectToPage("/Index"); // return  Page();
        }            
        
    }    
    
    public class ContactFormModel
    {

        [Display(Name = "Your Name")]
        [Required(ErrorMessage = "{0} is a required.")]
        [StringLength(40, ErrorMessage = "{0} cannot be longer than 40 characters.")]
        public string UserName { get; set; }


        [Display(Name = "Your Postcode")]
        [Required(ErrorMessage = "{0} is a required.")]
        [StringLength(12, ErrorMessage = "{0} cannot be longer than 12 characters.")]
        public string UserPostcode { get; set; }


        [Display(Name = "Your Email Address")]
        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(40, ErrorMessage = "{0} cannot be longer than 40 characters.")]
        public string UserEmailAddress { get; set; }

        [Display(Name = "Your Phone Number")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(15, ErrorMessage="{0} cannot exceed 15 characters")]
        public string UserPhoneNumber { get; set; }

        [Display(Name = "Your Message")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(1000, ErrorMessage = "{0} cannot be longer than 1000 characters.")]
        public string UserMessage { get; set; }
    }
}
