using Microsoft.IdentityModel.Tokens;
using Project.WebApi.Models.Domain;
using Project.WebApi.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.WebApi.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(UserDto userDto);
    }
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> CreateTokenAsync(UserDto userDto)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, userDto.Name));

            claims.Add(new Claim(ClaimTypes.Email, userDto.Email));
            userDto.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
