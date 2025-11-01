
namespace Company_Site.Application.DTOs
{
    public class AddUserRole
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public List<RoleItemDto> Roles { get; set; } = new();
    }
}
