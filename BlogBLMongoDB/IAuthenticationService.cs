using BlogDAL.Models;
using BlogDAL.Models.DTO;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> ExternalAuthenticateUser(ExternalAuthDTO model);
        string GenerateJwtToken(User user);
        Task<AuthenticationResponse> InternalAuthenticateUser(AuthenticationRequest model);
        Task<User> VerifyFacebookToken(ExternalAuthDTO model);
        Task<User> VerifyGoogleToken(ExternalAuthDTO model);
    }
}