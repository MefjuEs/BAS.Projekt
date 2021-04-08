using System.Threading.Tasks;

namespace BAS.AppServices
{
    public interface INotificationService
    {
        Task SendEmailConfirmation();
    }
}