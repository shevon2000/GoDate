using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoDate.API.Entities.Domain;
using Microsoft.IdentityModel.Tokens;

namespace GoDate.API.Services
{
    public class TokenService(IConfiguration config) : ITokenService  // IConfiguration for reading appsettings
    {
        public string CreateToken(User user)
        {
            var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");

            if (tokenKey.Length < 64)   // For HMAC-SHA512
            {
                throw new Exception("TokenKey must be at least 64 characters long");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));   // Store in a byte array

            // Create a list of claims(data) to embed inside the token
            // Here, we're including the user's username as the NameIdentifier
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            // Use the key to create signing credentials with HMAC-SHA512 algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor   // What the token contains and how it's signed
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),       // Token expiration time
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);              // Convert the token object into a compact JWT string and return it
        }
    }
}
