using BAS.AppServices.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAS.AppServices.Services.Interfaces
{
    public interface IAuthService
    {
        Task ChangeUserRole(long userId, long roleId);
        Task<bool> ConfirmRegistration(string userId, string token);
        Task<List<IdentityRole<long>>> GetRoles();
        Task<AdminUserListDTO> GetUserList(string query = null, int pageSize = 10, int pageNumber = 1);
        Task<List<UserDTO>> GetUsers(List<long> userIds = null);
        Task LogIn(LogInDTO loginDTO);
        Task LogOut();
        Task<RegisterResultDTO> Register(RegisterDTO registerDTO);
    }
}