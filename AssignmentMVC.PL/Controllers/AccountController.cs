using AssignmentMVC.DAL.Models;
using AssignmentMVC.PL.ViewModels.AccountModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentMVC.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = viewModel.UserName,
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
        public IActionResult Login()
        {
            return View();
        }
        #endregion
    }
}
