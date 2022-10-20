using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.WebApi.Data;
using Project.WebApi.Models.DTO;

namespace Project.WebApi.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> Authenticate(string Username, string Password);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Authenticate(string Username, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == Username && x.Password == Password);
            if(user == null)
            {
                return null;
            }
            var userRoles = await _context.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
            var userDto = _mapper.Map<UserDto>(user);
            if(userRoles.Any())
            {
                userDto.Roles = new List<string>();
                foreach (var userRole in userRoles)
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        userDto.Roles.Add(role.Name);
                    }
                }
            }
            return userDto;
        }
    }
}
