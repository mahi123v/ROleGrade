using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolesGrade.Models;
using YourNamespace.Data;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity.Data;
namespace RolesGrade.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;  // Inject IConfiguration

        // Constructor for dependency injection
        public LoginController(ILogger<LoginController> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;  // Inject IConfiguration
        }

        // API endpoint for user registration (signup)
        [HttpPost]
        [Route("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return BadRequest("User data is required.");

                // Check if the user already exists (by UserName or Email)
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == user.UserName || u.Email == user.Email);
                if (existingUser != null)
                {
                    return BadRequest("User with this UserName or Email already exists.");
                }

                // Hash the password before saving it to the database
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Create a new User object to save
                var newUser = new User
                {
                    UserId = user.UserId, // Generate UserId if not provided
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = hashedPassword,
                    Phone = user.Phone,
                    Age = user.Age,
                    BusinessId = user.BusinessId
                };

                // Save the user to the database
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to register user: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // API endpoint for user login
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                // Check if the user exists using either UserName or Email
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.UserName || u.Email == login.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                {
                    return Unauthorized("Invalid Username or Password");
                }

                // Generate JWT Token if user is authenticated
                var token = GenerateJwtToken(user);

                // Return the token to the user
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to login user: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // API endpoint for user login
        [HttpPost]
        [Route("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPasswordRequest)
        {
            try

            {
                if (forgotPasswordRequest.Email == null)
                    return BadRequest("email is required");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordRequest.Email);

                if (user == null)
                {
                    return BadRequest("user didnt find in this email");
                }

                var resetpassword = GenerateJwtToken(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, $"{ex.Message}");
            }
        }

        // Method to generate JWT token
        private string GenerateJwtToken(User user)
        {
            // Set the claims (user info) that will be added to the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Create the secret key using the value in appsettings
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate the JWT token with specified claims, expiry, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),  // Set token expiration time
                signingCredentials: credentials
            );

            // Return the JWT token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
