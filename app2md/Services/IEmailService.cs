using app2md.Models;

namespace app2md.Services
{
    public interface IEmailService
    {
        void SendMail(ContactFormViewModel model, int contactFormId);
    }
}
