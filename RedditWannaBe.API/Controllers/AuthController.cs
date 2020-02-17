using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedditWannaBe.API.Models;
using RedditWannaBe.DAL.Entities;
using RedditWannaBe.DAL.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Noter.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repository;
        private readonly IConfiguration configuration;

        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegistration userForRegistration)
        {
            userForRegistration.Username = userForRegistration.Username.ToLower();

            if (await repository.UserExists(userForRegistration.Username))
            {
                return BadRequest("User already exists");
            }

            var userToCreate = new User
            {
                Username = userForRegistration.Username
            };

            var createdUser = await repository.Register(userToCreate, userForRegistration.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            var userFromRepo = await repository.Login(userForLogin.Username, userForLogin.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var token = CreateToken(userFromRepo);

            return Ok(new
            {
                token
            });
        }

        private string CreateToken(User userFromRepo)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
