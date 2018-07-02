using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Orders.Core.Domain;
using Orders.Infrastructure.Dtos;
using Orders.Infrastructure.Services.Interfaces;
using Orders.Infrastructure.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Orders.Infrastructure.Extensions;

namespace Orders.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }
        public JsonWebTokenDto CreateToken(Guid userId, Role role)
        {
            var now = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebTokenDto
            {
                AccessToken = token,
                Expires = expires.ToTimestamp()
            };
        }
    }
}