using System.Threading.Tasks;

namespace BAS.AppServices
{
    public interface IAuthService
    {
        Task<bool> ConfirmRegistration(string userId, string token);
        Task<LogInResult> LogIn(LogInDTO loginDTO);
        Task LogOut();
        Task<RegisterResultDTO> Register(RegisterDTO registerDTO);
    }
}