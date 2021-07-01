using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticeCore.Models;
using PracticeCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("Sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }
        [Route("Sign-up")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel signup)
        {
            if (ModelState.IsValid)
            {
                var result= await _accountRepository.CreatUser(signup);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(signup);
                }
            }
            return RedirectToAction("ConfirmEmail",new { email=signup.Email});
        }
        [Route("Log-In")]
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [Route("Log-In")]
        [HttpPost]
        public async Task<IActionResult> LogIn(SignInModel signin, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result =await _accountRepository.LogIn(signin);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Book");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not Allowed");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
                
            }

            return View(signin);
        }
        [Route("Log-Out")]
        public async Task<IActionResult> LogOut()
        {
            await _accountRepository.SignOut();
            return RedirectToAction("Index", "Book");
        }
        [Route("Change-Password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Route("Change-Password")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                var result =await _accountRepository.ChangePassword(changePasswordModel);
                if (result.Succeeded)
                {
                    ViewBag.success = true;
                    ModelState.Clear();
                    return View();
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }
            return View(changePasswordModel);
        }

        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email
            };
            if(!string.IsNullOrEmpty(uid)&& !string.IsNullOrEmpty(token))
            {
                token = token.Replace(" ", "+");
                var result=await _accountRepository.ConfirmEmail(uid, token);
                if (result.Succeeded)
                {
                    model.EmailVerified = true;
                }
            }
            return View(model);
        }
        [Route("confirm-email")]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.FindUserByEmail(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }
                await _accountRepository.GenerateEmailConfirmationToken(user);
                model.EmailSend = true;
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return View(model);
        }
        [AllowAnonymous]
        [Route("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user =await _accountRepository.FindUserByEmail(model.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateForgotPasswordToken(user);
                }
                model.EmailSend = true;
            }
            return View(model);
        }
        [AllowAnonymous]
        [Route("reset-password")]
        public IActionResult ResetPassword(string uid,string token)
        {
            ResetPasswordModel model = new ResetPasswordModel()
            {
                UserId = uid,
                Token=token
            };
            return View(model);
        }
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(" ", "+");
                var result =await _accountRepository.ResetPassword(model);
                if (result.Succeeded)
                {
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
    }
}
