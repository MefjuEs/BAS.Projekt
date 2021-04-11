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
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole<long>> roleManager;
        private readonly INotificationService notificationService;

        public AuthService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole<long>> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task LogIn(LogInDTO loginDTO)
        {
            var user = await this.userManager.FindByEmailAsync(loginDTO.Email);

            if (user != null)
            {
                if (await this.userManager.CheckPasswordAsync(user, loginDTO.Password))
                {
                    var result = await this.signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

                    if (result.Succeeded)
                    {
                        return;
                    }
                }
            }

            throw new BASLogInException("Nieprawidłowy email lub hasło");
        }

        public async Task<RegisterResultDTO> Register(RegisterDTO registerDTO)
        {
            var result = new RegisterResultDTO();

            var userWithSameUserName = await userManager.FindByNameAsync(registerDTO.Username);
            var userWithSameEmail = await userManager.FindByEmailAsync(registerDTO.Email);
            var wasPasswordsIdentical = registerDTO.Password.Equals(registerDTO.ConfirmedPassword);

            var isPaswordValid = true;
            var validators = this.userManager.PasswordValidators;

            foreach (var validator in validators)
            {
                var validateResult = await validator.ValidateAsync(this.userManager, null, registerDTO.Password);

                if (!validateResult.Succeeded)
                {
                    isPaswordValid = false;
                    break;
                }
            }

            if (userWithSameUserName == null && userWithSameEmail == null && isPaswordValid && wasPasswordsIdentical)
            {
                var user = new ApplicationUser
                {
                    UserName = registerDTO.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = registerDTO.Name,
                    Surname = registerDTO.Surname,
                    Email = registerDTO.Email,
                };

                //using (var transaction = this.userRepository.Context.Database.BeginTransaction())
                {
                    var createResult = await userManager.CreateAsync(user, registerDTO.Password);

                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, UserRole.User.ToString());
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                        result.Id = user.Id.ToString();
                        result.Token = token;

                        await notificationService.SendEmailConfirmation();
                        //await this.notificationService.CreateNotification(NotificationType.REGISTRATION_CONFIRM, new RegistrationConfirmNotificationArgs
                        //{
                        //    UserAccountId = user.Id,
                        //    Token = token,
                        //});

                        //transaction.Commit();
                        result.Success = true;
                    }
                }
            }

            return result;
        }

        public async Task<bool> ConfirmRegistration(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user.EmailConfirmed)
            {
                return false;
            }

            if (user == null)
            {
                return false;
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task LogOut()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task ChangeUserRole(long userId, long roleId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new BASNotFoundException("Nie znaleziono takiego użytkownika");
            }

            var oldRole = await userManager.GetRolesAsync(user);
            var newRole = await roleManager.FindByIdAsync(roleId.ToString());
            await userManager.RemoveFromRoleAsync(user, oldRole.FirstOrDefault().ToString());
            await userManager.AddToRoleAsync(user, newRole.Name);

            await userManager.UpdateSecurityStampAsync(user);
        }

        public async Task<AdminUserListDTO> GetUserList(string query = null, int pageSize = 10, int pageNumber = 1)
        {
            throw new NotImplementedException();
            //var users = await this.userRepository.GetUserList(query, pageSize, pageNumber);
            //return users;
        }

        public async Task<List<UserDTO>> GetUsers(List<long> userIds = null)
        {
            throw new NotImplementedException();
            //var users = await this.userRepository.GetUsers(userIds);
            //return users;
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
    }
}
