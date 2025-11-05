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
        Task<IEnumerable<string>> GetAllUserRoleAsync(Guid id);
        Task<ResultDto> CreateUserAsync(RegisterDto dto);
        Task<UserEditDto> GetEditUserAsync(Guid id);
        Task<ResultDto> EditUserAsync(UserEditDto dto);
        Task<UserDeleteDto> GetDeleteUserAsync(Guid id);
        Task<ResultDto> DeleteUserAsync(UserDeleteDto dto);
        Task<AddUserRole> GetAddUserRole(Guid id);
        Task<ResultDto> AddUserRole(AddUserRole dto);
    }
}
