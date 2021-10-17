using TechchainDAL.Models;
using TechchainDAL.Models.DTO;
using System.Threading.Tasks;

namespace TechchainBL
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