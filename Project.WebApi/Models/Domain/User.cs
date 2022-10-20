namespace Project.WebApi.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string  Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        //nav prop
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
