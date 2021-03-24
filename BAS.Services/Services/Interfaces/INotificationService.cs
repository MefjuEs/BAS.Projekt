using System.Threading.Tasks;

namespace BAS.AppServices.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailConfirmation();
    }
}