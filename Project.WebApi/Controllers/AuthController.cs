using Microsoft.AspNetCore.Mvc;
using Project.WebApi.Models.DTO;
using Project.WebApi.Repositories;

namespace Project.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginRequest request)
        {
            var userDto = await _userRepository.Authenticate(request.Username, request.Password);
            if (userDto != null)
            {
                // Generate a JWT Token
                var token = await _tokenHandler.CreateTokenAsync(userDto);
                return Ok(token);
            }
            return BadRequest("Username or Password is incorrect.");
        }
    }
}