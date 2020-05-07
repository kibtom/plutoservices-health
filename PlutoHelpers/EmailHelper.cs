using Microsoft.Extensions.Configuration;

namespace PlutoHealthWeb.PlutoHelpers
{
    public class EmailHelper
    {
        //    "Host": "mail.plutobvservices.co.uk",
        //     "Port": 587,
        //     "Email": "no-reply@plutobvservices.co.uk",
        //     "Passwword": "plutobv@2019",
        //     "IsEmailEnabled": true

        public string Server {get; set;}
        public int Port {get; set;}
        public string SenderEmail {get; set;}
        public string SenderName{get; set;}
        public string Password{get; set;}
        public string IsEmailEnabled{get; set;}

        // public EmailHelper(IConfiguration iConfiguration)
        // {
        //     var smtpSection = iConfiguration.GetSection("PlutoSmtpSettings");
        //     if(smtpSection != null)
        //     {
        //         host = smtpSection.GetSection("Host").Value;
        //         port = smtpSection.GetSection("Port").Value;
        //         email = smtpSection.GetSection("Email").Value;
        //         password = smtpSection.GetSection("Password").Value;
        //         isEmailEnabled = smtpSection.GetSection("IsEmailEnabled").Value;
        //     }
        // }
        
    }
}