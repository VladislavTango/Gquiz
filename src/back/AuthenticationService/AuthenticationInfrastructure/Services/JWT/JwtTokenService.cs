using AuthenticationInfrastructure.Interface.Service;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationInfrastructure.Services.JWT
{
    public class JwtTokenService : IJwtTokentService
    {
        private readonly string secretKey = "VQF3Dv69xcmTZqK4g6gepHZYSLSHT9G2";
        private readonly string issuer = "keklik";
        private readonly string audience = "Yabloki";
        private readonly TimeSpan expiration = TimeSpan.FromDays(30);

        public string GenerateToken(Guid userId, string userName, string Role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role , Role)
            };

            var token = new JwtSecurityToken
                (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(expiration),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
