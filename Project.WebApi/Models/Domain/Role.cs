namespace Project.WebApi.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }

        //nav prop
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
