using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ERP.Application.Core.Helpers
{
    public static class JWTHelper
    {
        public static string GenerateJwtToken(string nameId, string secretKey)
        {
            var claims = new List<Claim>
            {
                new Claim("nameid", nameId),
            };

            // generate token that is valid for 2 days
            var tokenHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 24)
            {
                throw new ArgumentException("JWT secret key not available.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = "ERPAPI",
                Audience = "ERPWEB",
                IssuedAt = DateTime.Now,
                TokenType = "JWT",
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static IEnumerable<Claim> ValidateTokenWithLifeTime(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 24)
            {
                throw new ArgumentException("JWT secret key not available.");
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = "ERPAPI",
                ValidateAudience = true,
                ValidAudience = "ERPWEB",
                ValidateLifetime = true,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims;
        }

        public static IEnumerable<Claim> ValidateTokenWithoutLifeTIme(string token, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 24)
            {
                throw new ArgumentException("JWT secret key not available.");
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = "ERPAPI",
                ValidateAudience = true,
                ValidAudience = "ERPWEB",
                // For refresh token, no need to validate life time
                ValidateLifetime = false,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var randomText = RandomNumberGenerator.Create();
            randomText.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}