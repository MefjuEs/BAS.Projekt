using BAS.AppCommon;
using BAS.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAS.AppServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole<long>> roleManager;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task ChangeUserRole(long userId, long roleId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new BASNotFoundException("User not found");
            }

            var oldRole = await userManager.GetRolesAsync(user);
            var newRole = await roleManager.FindByIdAsync(roleId.ToString());
            await userManager.RemoveFromRoleAsync(user, oldRole.FirstOrDefault().ToString());
            await userManager.AddToRoleAsync(user, newRole.Name);

            await userManager.UpdateSecurityStampAsync(user);
        }

        public async Task<List<IdentityRole<long>>> GetRoles()
        {
            var roles = await this.roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<bool> DoesUserExist(long id)
        {
            return (await userManager.FindByIdAsync(id.ToString())) != null;
        }

        public async Task<string> GetUsername(long id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return "Gal Anonim";

            return user.UserName;
        }

        public async Task<UserDTO> GetUser(long id)
        {
            var user = await this.userManager.FindByIdAsync(id.ToString());

            var result = new UserDTO
            {
                Email = user.Email,
            };

            return result;
        }
    }
}
