using Microsoft.AspNetCore.Identity;
using PracticeCore.Models;
using System.Threading.Tasks;

namespace PracticeCore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreatUser(SignUpUserModel user);
        Task<SignInResult> LogIn(SignInModel signin);
        Task SignOut();
        Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel);
        Task<IdentityResult> ConfirmEmail(string uid, string token);
        Task<ApplicationUser> FindUserByEmail(string email);
        Task GenerateEmailConfirmationToken(ApplicationUser user);
        Task GenerateForgotPasswordToken(ApplicationUser user);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);
    }
}