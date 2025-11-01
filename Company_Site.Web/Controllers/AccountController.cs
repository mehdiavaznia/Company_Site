using Company_site.Domain.Entities;
using Company_Site.Application.DTOs;
using Company_Site.Application.Interfaces;
using Company_Site.Infrastructure.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;

namespace Company_Site.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var result = await _accountService.RegisterAccountAsync(register);
            if (result.IsSuccess) 
            {
                return RedirectToAction("Login", "Account");
            }
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl ="/") 
        {
            var result = await _accountService.GetAccountAsync(returnUrl);

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login) 
        {
            var user = await _accountService.LoginAccountAsync(login);

            if (user.IsSuccess) 
            {
                return Redirect(login.ReturnUrl);
            }
            return View();
        }

        public async Task<IActionResult> LogOut() 
        {
            var result = await _accountService.LogOutAccountAsync();
            if (result.IsSuccess) 
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("index");
            
        }

        //public IActionResult VerifyEmail() 
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> VerifyEmail(VerifyEmailDto verifyEmail)
        //{
        //    if (ModelState.IsValid) 
        //    {
        //        var user = await _userManager.FindByNameAsync(verifyEmail.Email);

        //        if (user==null)
        //        {
        //            ModelState.AddModelError("", "Something is wrong!");
        //            return View(verifyEmail);
        //        }
        //        else 
        //        {
        //            return RedirectToAction("ChangePassword", "Account", new { UserName = verifyEmail.Email });
        //        }
        //    }
        //    return View(verifyEmail);
        //}
        //public IActionResult ChangePassword(string username) 
        //{
        //    if (string.IsNullOrEmpty(username))
        //    {
        //        return RedirectToAction("VerifyEmail", "Account");
        //    }
        //    return View(new ChangePasswordDto { Email = username });
        //}
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordDto changePassword)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(changePassword.Email);
        //        if (user != null)
        //        {
        //            var result = await _userManager.RemovePasswordAsync(user);
        //            if (result.Succeeded)
        //            {
        //                result = await _userManager.AddPasswordAsync(user, changePassword.NewPassword);
        //                return RedirectToAction("LogIn", "Account");
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError("", error.Description);
        //                }

        //                return View(changePassword);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Email not found");
        //            return View(changePassword);
        //        }
        //    }
        //    else 
        //    {

        //            ModelState.AddModelError("", "Something went Wrong!");
        //        return View(changePassword);
        //    }
        //}
    }
}
