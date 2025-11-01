using Company_site.Domain.Entities;
using Company_Site.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<IEnumerable<string>> GetAllUserRoleAsync(string id);
        Task<ResultDto> CreateUserAsync(RegisterDto dto);
        Task<UserEditDto> GetEditUserAsync(string id);
        Task<ResultDto> EditUserAsync(UserEditDto dto);
        Task<UserDeleteDto> GetDeleteUserAsync(string id);
        Task<ResultDto> DeleteUserAsync(UserDeleteDto dto);
        Task<AddUserRole> GetAddUserRole(string id);
        Task<ResultDto> AddUserRole(AddUserRole dto);
    }
}
