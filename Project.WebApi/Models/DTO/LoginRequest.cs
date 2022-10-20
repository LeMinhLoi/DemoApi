using Microsoft.AspNetCore.Mvc;

namespace Project.WebApi.Models.DTO
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
