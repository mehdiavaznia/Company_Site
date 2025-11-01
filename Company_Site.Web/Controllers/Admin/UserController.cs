using Company_site.Domain.Entities;
using Company_Site.Application.DTOs;
using Company_Site.Application.DTOs.Roles;
using Company_Site.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Company_Site.Web.Controllers.Admin
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUserAsync();
               
            return View(users);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterDto register) 
        {
            if (!ModelState.IsValid) 
            {
                return View(register);
            }
            var result = await _userService.CreateUserAsync(register);
            if (!result.IsSuccess) 
            {
                TempData["Message"] = result.Message;
                return View(register);
            }


             return RedirectToAction("Index", "User");

           
        }
        public async Task<IActionResult>Edit(string Id) 
        {
            var userEdit = await _userService.GetEditUserAsync(Id);
            if (userEdit == null) 
            {
                TempData["Message"] = "کاربر مورد نظر یافت نشد";
                return RedirectToAction("Index");
            }
            return View(userEdit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditDto userEdit) 
        {
            if (!ModelState.IsValid) 
            {
                return View(userEdit);
            }
            var result = await _userService.EditUserAsync(userEdit);
            if (!result.IsSuccess) 
            {
                TempData["Message"] = result.Message;
                return View(userEdit);
            }
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(string id) 
        {
            var userDelete = await _userService.GetDeleteUserAsync(id);
            if (userDelete == null) 
            {
                TempData["Message"] = "کاربر مورد نظر یافت نشد";
                return RedirectToAction("Index");
            }
            return View(userDelete);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteDto userDelete) 
        {
            var result = await _userService.DeleteUserAsync(userDelete);
            if (!result.IsSuccess) 
            {
                TempData["Message"] = result.Message;
                return View(userDelete);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddUserRole(string Id) 
        {
            var user = await _userService.GetAddUserRole(Id);
            if (user == null) 
            {
                TempData["Message"] = "کاربر مورد نظر یافت نشد";
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserRole(AddUserRole addRole) 
        {
            var result = await _userService.AddUserRole(addRole);
            return RedirectToAction("UserRoles", "User");

        }

        public async Task<IActionResult> UserRoles(string Id) 
        {
            var result = await _userService.GetAllUserRoleAsync(Id);
            return View(result);
        }
    }
}
