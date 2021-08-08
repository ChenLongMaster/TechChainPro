using BlogDAL.Models;
using BlogDAL.Models.DTO;
using BlogDAL.UnitOfWork;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
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
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthenticationService(BlogContext blogContext, IConfiguration configuration, IUserService userService)
        {
            _blogContext = blogContext;
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<AuthenticationResponse> InternalAuthenticateUser(AuthenticationRequest model)
        {
            var entity = await _userService.GetUserByNameOrEmail(new User() { Username = model.Username });

            if (entity is not null)
            {
                string hashedClientPassword = GenerateSaltedHash(model.Password, entity.Salt);
                string unescapedEntiryPassword = Regex.Unescape(entity.Password);
                if (hashedClientPassword.Equals(unescapedEntiryPassword))
                {
                    string token = GenerateJwtToken(entity);
                    return new (token.ToString()); ;
                }
            }

            return null;
        }
        public async Task<User> VerifyGoogleToken(ExternalAuthDTO model)
        {
            GoogleJsonWebSignature.ValidationSettings setting = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _configuration["GoogleAuthSettings:clientId"] }
            };

            GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(model.Token, setting);

            var user = new User()
            {
                Username = payload.Name,
                Email = payload.Email,
                Avatar = payload.Picture,
                Provider = "GOOGLE"
            };

            return user;
        }

        public async Task<AuthenticationResponse> GoogleAuthenticateUser(ExternalAuthDTO model)
        {
            var userSocial = await VerifyGoogleToken(model);

            if (userSocial is null)
            {
                return null;
            }
            User userLocal = await _userService.GetUserByNameOrEmail(userSocial);
            if (userLocal is null)
            {
                bool createSucess = await _userService.CreateUser(userSocial);
                if (createSucess)
                {
                    string token = GenerateJwtToken(userLocal);
                    return new AuthenticationResponse(token.ToString());
                }
            }
            else
            {
                string token = GenerateJwtToken(userLocal);
                return new AuthenticationResponse(token.ToString());
            }
            return null;
        }

        public async Task<AuthenticationResponse> FacebookAuthenticateUser(ExternalAuthDTO model)
        {
            var userSocial = await VerifyFacebookToken(model);

            if (userSocial is null)
            {
                return null;
            }
            var userLocal = await _userService.GetUserByNameOrEmail(userSocial);
            if (userLocal is null)
            {
                await _userService.CreateUser(userSocial);
                var createdUser = await _userService.GetUserByNameOrEmail(userSocial);
                if (createdUser is not null)
                {
                    var token = GenerateJwtToken(createdUser);
                    return new AuthenticationResponse(token);
                }
            }
            else
            {
                var token = GenerateJwtToken(userLocal);
                return new AuthenticationResponse(token);
            }
            return null;
        }

        public async Task<User> VerifyFacebookToken(ExternalAuthDTO model)
        {
            User user = new User();
            HttpClient client = new HttpClient();

            string verifyTokenEndPoint = string.Format("https://graph.facebook.com/me?access_token={0}&fields=email,name", model.Token);
            string verifyAppEndpoint = string.Format("https://graph.facebook.com/app?access_token={0}", model.Token);

            HttpResponseMessage response = await client.GetAsync(new Uri(verifyTokenEndPoint));

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic userObj = JsonConvert.DeserializeObject(content);

                response = await client.GetAsync(new Uri(verifyAppEndpoint));
                content = await response.Content.ReadAsStringAsync();
                dynamic appObj = JsonConvert.DeserializeObject(content);

                string getPicEndPoint = string.Format("https://graph.facebook.com/v5.0/me/picture?redirect=false&type=large&access_token={0}", model.Token);
                HttpResponseMessage userPictureRes = client.GetAsync(new Uri(getPicEndPoint)).Result;
                string userPictureContent = await userPictureRes.Content.ReadAsStringAsync();
                dynamic userPicture = JsonConvert.DeserializeObject<dynamic>(userPictureContent);

                string pictureUrl = userPicture.data.url.ToString();

                if (appObj["id"] == _configuration["FacebookAuthSettings:clientId"])
                {
                    user.Email = userObj["email"];
                    user.Username = userObj["name"];
                    user.Avatar = pictureUrl;
                    user.Provider = "FACEBOOK";
                }
                else
                {
                    return null;
                }
            }

            return user;
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
            var userClaims = new ClaimsIdentity(new List<Claim> {
                new ("id",user.Id.ToString()),
                new ("username",user.Username),
                new ("email",user.Email == null ? "" : user.Email),
                new ("avatar",user.Avatar == null ? "" : user.Avatar)
            }); ;

            foreach (var role in user.Roles)
            {
                userClaims.AddClaim(new (ClaimTypes.Role, role.Name));
            }
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = userClaims,
                SigningCredentials = credential,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
