using BlogDAL.Models;
using BlogDAL.Models.DTO;
using Google.Apis.Auth;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> InternalAuthenticateUser(AuthenticationRequest model);
        Task<AuthenticationResponse> ExternalAuthenticateUser(ExternalAuthDTO model);
    }
}