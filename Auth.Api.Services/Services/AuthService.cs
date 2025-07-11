using Auth.Api.Services.Interfaces;
using Auth.Api.Services.Model;
using Auth.Repositories.Interfaces;
using Core.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IAuthRepositories _authRepositories;

        public AuthService(
            IOptions<JwtConfig> config,
            IAuthRepositories authRepositories
            )
        {
            _jwtConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
            _authRepositories = authRepositories ?? throw new ArgumentNullException(nameof(authRepositories));
        }

        
        public async Task<LoginDto> AuthenticateAsync(string username, string password)
        {
            username.ValidateArgumentOrThrow(nameof(username));

            var user = await _authRepositories.GetByUsernameAsync(username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return GenerateToken(username);
        }

        private LoginDto GenerateToken(string userName)
        {

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.ToLocalTime().AddMinutes(_jwtConfig.ExpireMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
           
            var tokenString =  new JwtSecurityTokenHandler().WriteToken(token);

            return new LoginDto
            {
                Token = tokenString,
                ExpiresAt = expires
            };
        }
    }
}
