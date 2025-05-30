using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace stepTogether.Utils
{
    public static class JwtHelper
    {
        private static string SecretKey = "THIS_IS_A_VERY_STRONG_SECRET_KEY_123!"; // 至少 32 字元

        public static string GenerateToken(string userId, string email, string role, int expireDays = 7)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-app",
                audience: "your-app",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expireDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static TokenValidationParameters GetValidationParameters() => new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "your-app",
            ValidateAudience = true,
            ValidAudience = "your-app",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            ValidateIssuerSigningKey = true
        };
    }
}
