using System.Threading.Tasks;
using hp_proj_1_backend_master.Models;

namespace hp_proj_1_backend_master.Services.MailService
{
    public interface IMailService
    {
         Task SendEmailAsync(MailRequest mailRequest);
    }
}