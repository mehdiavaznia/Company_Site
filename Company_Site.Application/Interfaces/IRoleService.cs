using Company_Site.Application.DTOs;
using Company_Site.Application.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleListDto>> GetRoleLists();
        Task<ResultDto> CreateRoleAsync(AddNewRoleDto dto);
        Task<ResultDto> EditRoleAsync(RoleEditDto dto);
        Task<IEnumerable<UserDto>> UserInRole(string name);
        Task<RoleEditDto> GetEditRoleAsync(string id);
        Task<ResultDto> DeleteRoleAsync(RoleDeleteDto dto);
        Task<RoleDeleteDto> GetDeleteRoleAsync(string id);
    }
}
