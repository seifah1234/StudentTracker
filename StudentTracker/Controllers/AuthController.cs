using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StudentTracker.BLL.Interfaces;
using StudentTracker.DAL.Data;
using StudentTracker.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ITeacherService teacherService, IConfiguration configuration, AppDbContext context)
        {
            _teacherService = teacherService;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Teacher loginRequest)
        {
            var teacher = _context.Teachers
                .FirstOrDefault(t => t.Email == loginRequest.Email && t.PasswordHash == loginRequest.PasswordHash);

            if (teacher == null || teacher.PasswordHash != loginRequest.PasswordHash)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is missing."));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    
                    new Claim(ClaimTypes.Name, teacher.Name),
                    new Claim(ClaimTypes.Role, "Teacher")
                }),
                Expires=DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                Issuer=_configuration["Jwt:Issuer"],
                Audience=_configuration["Jwt:Audience"]
            };
            var token =tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken=tokenHandler.WriteToken(token);
            return Ok(new { token = jwtToken });
        }
    }
}