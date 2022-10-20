namespace Project.WebApi.Models.Domain
{
    public class UserRole
    {
        public Guid Id { get;set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }


        //nav prop
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
