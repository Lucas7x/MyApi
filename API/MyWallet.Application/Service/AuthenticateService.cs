using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyWallet.Application.Interfaces;

namespace MyWallet.Application.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IPasswordService _passwordService;

        public AuthenticateService(IUserRepository userRepository, IAppSettingsProvider appSettingsProvider, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _appSettingsProvider = appSettingsProvider;
            _passwordService = passwordService;
        }

        public bool Authenticate(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                return false;

            if (_passwordService.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return true;

            return false;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_appSettingsProvider.GetJwtSigningKey()));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddMinutes(_appSettingsProvider.GetJwtExpirationTimeInMinutes());

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _appSettingsProvider.GetJwtIssuer(),
                audience: _appSettingsProvider.GetJwtAudience(),
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool UserExists(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
                return false;

            return true;
        }
    }
}
