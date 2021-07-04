using BlogDAL.Models;
using BlogDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogBL
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly BlogContext _blogContext;
        private readonly AppSettings _appSettings;

        public AuthenticationService(BlogContext blogContext, IOptions<AppSettings> appSettings)
        {
            _blogContext = blogContext;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest model)
        {
            var entity = await _blogContext.Users.FirstOrDefaultAsync(x => x.Username == model.Username);

            if (entity is not null)
            {
                string hashedClientPassword = GenerateSaltedHash(model.Password, entity.Salt);
                string unescapedEntiryPassword = Regex.Unescape(entity.Password);
                if (hashedClientPassword.Equals(unescapedEntiryPassword))
                {
                    var token = GenerateJwtToken(entity);
                    return new AuthenticationResponse(entity, token); ;
                }

            }

            return null;
        }

        public static string GenerateSaltedHash(string plainText, string salt)
        {
            byte[] plainTextBytes = Encoding.ASCII.GetBytes(plainText);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);

            var algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainTextBytes[i];
            }

            for (int i = 0; i < saltBytes.Length; i++)
            {
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];
            }

            byte[] computedHash = algorithm.ComputeHash(plainTextWithSaltBytes);
            string result = Encoding.ASCII.GetString(computedHash);

            return result;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "https://localhost:5001",
                Audience = "https://localhost:5001",
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
