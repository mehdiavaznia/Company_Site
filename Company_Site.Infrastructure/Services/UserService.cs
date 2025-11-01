using Company_site.Domain.Entities;
using Company_Site.Application.DTOs;
using Company_Site.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResultDto> AddUserRole(AddUserRole dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null) 
            {
                return new ResultDto(false, "کاربر پیدا نشد");
            }
            var result = await _userManager.AddToRoleAsync(user, dto.Role);
            if (await _userManager.IsInRoleAsync(user, dto.Role)) 
            {
                return new ResultDto(false, "کاربر این نقش را دارد");
            }
            if (result.Succeeded) 
            {
                return new ResultDto(true, "نقش به کاربر مورد نظر اضافه شد");
            }
            return new ResultDto(false, string.Join("\n", result.Errors.Select(e => e.Description)));
        }

        public async Task<ResultDto> CreateUserAsync(RegisterDto dto)
        {
            User newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Email
            };
            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded) 
            {
                return new ResultDto(false, string.Join("\n", result.Errors.Select(e => e.Description)));
            }

            return new ResultDto(true, "کاربر ایجاد شد");
        }

        public async Task<ResultDto> DeleteUserAsync(UserDeleteDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null) 
            {
                return new ResultDto(false, "کاربر یافت نشد");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return new ResultDto(true, "کاربر حذف شد");
            }
            return new ResultDto(false, string.Join("\n", result.Errors.Select(e => e.Description)));
        }

        public async Task<ResultDto> EditUserAsync(UserEditDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Id);
            if (user == null) 
            {
                return new ResultDto(false, "کاربر یافت نشد");
            }
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.UserName = dto.UserName;
            user.Id = dto.Id;
  
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResultDto(true, "تغییرات شما اعمال شد");
            }
            return new ResultDto(false, string.Join("\n", result.Errors.Select(e => e.Description)));
        }

        public async Task<AddUserRole> GetAddUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var roles = new List<RoleItemDto>(
                _roleManager.Roles.Select(p => new RoleItemDto
                {
                    Text = p.Name,
                    Value = p.Name,
                })).ToList();
            var dto = new AddUserRole
            {
                Id = id,
                Roles = roles,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}"

            };
            return dto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _userManager.Users
                .Select(p => new UserDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    EmailConfirmed = p.EmailConfirmed,
                    AccessFailedCount = p.AccessFailedCount
                }).ToListAsync();
            return users;
        }

        public async Task<IEnumerable<string>> GetAllUserRoleAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<UserDeleteDto> GetDeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            UserDeleteDto userDelete = new UserDeleteDto()
            {
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Id = user.Id,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return userDelete;
        }

        public async Task<UserEditDto> GetEditUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) 
            {
                return null;
            }

            UserEditDto userEdit = new UserEditDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return userEdit;
        }
    }
}
