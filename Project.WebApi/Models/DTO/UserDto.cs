using Project.WebApi.Models.Domain;

namespace Project.WebApi.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<string> Roles{ get; set; }

    }
}
