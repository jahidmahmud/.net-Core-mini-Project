using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PracticeCore.Models;
using PracticeCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,IUserService userService,
            IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }
        public async Task<IdentityResult> CreatUser(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                Email = userModel.Email,
                UserName = userModel.Email,
                FirstName=userModel.FirstName,
                LasttName=userModel.LastName
            };
            var result=await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (!string.IsNullOrEmpty(token))
                {
                    await SendConfirmationEmail(user, token);
                }
            }
            return result;
        }
        public async Task<ApplicationUser> FindUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task GenerateEmailConfirmationToken(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendConfirmationEmail(user, token);
            }
        }
        public async Task GenerateForgotPasswordToken(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }
        public async Task<SignInResult> LogIn(SignInModel signin)
        {
            var result = await _signInManager.PasswordSignInAsync(signin.Email,signin.Password,signin.RememberMe,false);
            return result;
        }
        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var id = _userService.getUserId();
            var user =await _userManager.FindByIdAsync(id);
            return await _userManager.ChangePasswordAsync(user,changePasswordModel.CurrentPassword,changePasswordModel.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmail(string uid,string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }
        public async Task<IdentityResult> ResetPassword(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync( await _userManager.FindByIdAsync((model.UserId).ToString()), model.Token, model.NewPassword);
        }

        private async Task SendConfirmationEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain + confirmLink ,user.Id,token))
                }
            };
            await _emailService.ConfirmEmail(options);
        }
        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmLink = _configuration.GetSection("Application:ForgotPassword").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain + confirmLink ,user.Id,token))
                }
            };
            await _emailService.ForgotPasswordEmail(options);
        }
    }
}
