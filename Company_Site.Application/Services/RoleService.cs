using Company_site.Domain.Entities;
using Company_Site.Application.DTOs;
using Company_Site.Application.DTOs.Roles;
using Company_Site.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResultDto> CreateRoleAsync(AddNewRoleDto dto)
        {
            Role role = new Role()
            {
                PersianName = dto.PersianName,
                Name = dto.Name,
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) 
            {
                return new ResultDto(false, "مشکلی رخ داده است");
            }
            return new ResultDto(true, "نقش با موفقیت ایجاد شد");
        }

        public async Task<ResultDto> DeleteRoleAsync(RoleDeleteDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id);
            if (role == null) 
            {
                return new ResultDto(false, "نقش پیدا نشد");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new ResultDto(true, "نقش حذف شد");
            }
            return new ResultDto(false, string.Join("\n", result.Errors.Select(e => e.Description)));

        }

        public async Task<ResultDto> EditRoleAsync(RoleEditDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id.ToString());
            if (role == null) 
            {
                return new ResultDto(false, "نقش پیدا نشد");
            }

            role.Name = dto.Name;
            //role.Id = dto.Id;
            role.PersianName = dto.PersianName;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) 
            {
                return new ResultDto(true, "نقش با موفقیت آپدیت شد");
            }
            return new ResultDto(false, string.Join("\n", result.Errors.Select(e => e.Description)));
        }

        public async Task<RoleDeleteDto> GetDeleteRoleAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) 
            {
                return null;
            }

            RoleDeleteDto roleDelete = new RoleDeleteDto()
            {
                Id = role.ToString(),
                Name = role.Name,
                PersianName = role.PersianName
            };
            return roleDelete;
        }

        public async Task<RoleEditDto> GetEditRoleAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) 
            {
                return null;
            }

            RoleEditDto roleEdit = new RoleEditDto()
            {
                //Id = role.ToString(),
                Name = role.Name,
                PersianName = role.PersianName
            };
            return roleEdit;
        }

        public async Task<IEnumerable<RoleListDto>> GetRoleLists()
        {
            var roles = await _roleManager.Roles
                .Select(p =>
                new RoleListDto
                {
                    Id = p.Id.ToString(),
                    PersianName = p.PersianName,
                    Name = p.Name
                })
                .ToListAsync();
            return roles;
        }

        public async Task<IEnumerable<UserDto>> UserInRole(string name)
        {
            var user = await _userManager.GetUsersInRoleAsync(name);

            var result = user.Select(p => new UserDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                PhoneNumber = p.PhoneNumber,
                Id = p.Id
            });
            return result;
        }
    }
}
