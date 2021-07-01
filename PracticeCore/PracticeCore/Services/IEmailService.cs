using PracticeCore.Models;
using System.Threading.Tasks;

namespace PracticeCore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task ConfirmEmail(UserEmailOptions userEmailOptions);
        Task ForgotPasswordEmail(UserEmailOptions userEmailOptions);
    }
}