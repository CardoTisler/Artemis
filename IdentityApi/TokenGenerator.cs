using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace IdentityApi;

public class TokenGenerator
{
    // Ideally Guid userId would also be passed to make integration with other services easier.
    public string GenerateToken(string email, int userId)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var key = "this-really-should-be-an-env-variable"u8.ToArray();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, email), // subject
            new(JwtRegisteredClaimNames.Email, email),
            new("userId", userId.ToString()),
            new(JwtRegisteredClaimNames.Aud, "http://localhost:5084")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = "http://localhost:5081/",
            Audience = "http://localhost:5084/",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        return tokenHandler.CreateToken(tokenDescriptor);
    }
}