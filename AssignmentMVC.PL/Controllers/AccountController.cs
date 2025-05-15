using AssignmentMVC.DAL.Models;
using AssignmentMVC.PL.Utilities;
using AssignmentMVC.PL.ViewModels.AccountModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Email = AssignmentMVC.PL.Utilities.Email;

namespace AssignmentMVC.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager ) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = viewModel.Email.Split("@")[0],
                    Email = viewModel.Email,
                    IsAgree = viewModel.IsAgree,
                    FristName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                };
                
                var result = userManager.CreateAsync(user,viewModel.Password).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = userManager.FindByEmailAsync(viewModel.Email).Result;
           
            if (user is not null)
            {
                bool flag = userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                if (flag)
                {
                    var Result = signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Invalid login is not Allowed.");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Invalid login is LockedOut.");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(viewModel);
        }
        #endregion

        #region SignOut
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }

        public  IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user =  userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null)
                {
                    var Token = userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var link = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, token = Token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Your Passoword",
                        Body = link,

                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInBox));

                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Email.");
            return View(nameof(ForgetPassword), viewModel);

        }

        public IActionResult CheckYourInBox()
        {
            return View();
        }

        public IActionResult ResetPassword(string email ,string Token)
        {
            TempData["email"] = email;
            TempData["token"] = Token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);
            string email = TempData["email"] as string ?? string.Empty ;
            string token = TempData["token"] as string ?? string.Empty;
            var user = userManager.FindByEmailAsync(email).Result;
            if (user != null) 
            {
                var result = userManager.ResetPasswordAsync(user, token, viewModel.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(viewModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Email.");
                return View(nameof(ResetPassword),viewModel);
            }
        }
        #endregion
    }
}
