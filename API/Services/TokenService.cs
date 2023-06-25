using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTKey"]!));
        }

        public string CreateToken(User user, string existingToken = null)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName!)
            };

            var expireDate = string.IsNullOrEmpty(existingToken)
                ? DateTime.Now.AddDays(1)
                : GetExpireDate(existingToken);

            // Signing Key
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Descriping Token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = cred,
                Subject = new ClaimsIdentity(claims),
                Expires = expireDate
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private DateTime GetExpireDate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return tokenHandler.ValidTo;
        }
    }
}
