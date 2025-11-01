using Company_Site.Application.DTOs.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_Site.Application.DTOs
{
    public class AddUserRoleDto
    {
        public string Id { get; set; }
        public string Role { get; set; }
        //public List<SelectListItem> Roles { get; set; }
    }
}
