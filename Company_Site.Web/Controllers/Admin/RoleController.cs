using Company_site.Domain.Entities;
using Company_Site.Application.DTOs;
using Company_Site.Application.DTOs.Roles;
using Company_Site.Application.Interfaces;
using Company_Site.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Company_Site.Web.Controllers.Admin
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetRoleLists();
            return View(roles);

        }
        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddNewRoleDto newRole)
        {

            if (!ModelState.IsValid)
            {
                return View(newRole);
            }
            var result = await _roleService.CreateRoleAsync(newRole);
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.Message;
                return View(newRole);
            }
            return RedirectToAction("Index", "Role");
        }
        public async Task<IActionResult> Edit(string Id) 
        {
            var role = await _roleService.GetEditRoleAsync(Id);
            if (role == null)
            {
                TempData["Message"] = "کاربر مورد نظر یافت نشد";
                return RedirectToAction("Index");
            }
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleEditDto roleEdit) 
        {
            if (!ModelState.IsValid)
            {
                return View(roleEdit);
            }
            var result = await _roleService.EditRoleAsync(roleEdit);
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.Message;
                return View(roleEdit);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string Id) 
        {
            var roleDelete = await _roleService.GetDeleteRoleAsync(Id);
            if (roleDelete == null)
            {
                TempData["Message"] = "کاربر مورد نظر یافت نشد";
                return RedirectToAction("Index");
            }
            return View(roleDelete);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(RoleDeleteDto roleDelete) 
        {
            var result = await _roleService.DeleteRoleAsync(roleDelete);
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.Message;
                return View(roleDelete);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UserInRole(string Name) 
        {
            var users = await _roleService.UserInRole(Name);
            return View(users);
        }
    }
}
