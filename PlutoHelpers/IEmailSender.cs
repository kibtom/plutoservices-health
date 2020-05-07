using System.Threading.Tasks;
namespace PlutoHealthWeb.PlutoHelpers
{
    public interface IEmailSender
    {
         Task SendEmailAsyc(string email, string subject, string message);  
    }
}