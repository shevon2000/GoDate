using System.Security.Cryptography;
using System.Text;
using GoDate.API.Data;
using GoDate.API.Entities.Domain;
using GoDate.API.Entities.DTO.Auth;
using GoDate.API.Services.TokenService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoDate.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ApplicationDbContext context;
        private readonly ITokenService service;

        public AccountController(ApplicationDbContext context, ITokenService service)
        {
            this.context = context;
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("Username is already taken");
            }
            return Ok();

            //using var hmac = new HMACSHA512();

            //var user = new User
            //{
            //    UserName = registerDto.UserName.ToLower(),
            //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            //    PasswordSalt = hmac.Key
            //};

            //await context.Users.AddAsync(user);
            //await context.SaveChangesAsync();

            //return new UserDto
            //{
            //    UserName = user.UserName,
            //    Token = service.CreateToken(user)
            //};
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await context.Users
                .Include(p => p.Photos)
                    .FirstOrDefaultAsync(x => 
                        x.UserName == loginDto.UserName.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // Compare the computed hash with the stored hash
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }

            return new UserDto
            {
                UserName = user.UserName,
                Token = service.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url   // Navbar avatar
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
