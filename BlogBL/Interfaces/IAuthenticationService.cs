using BlogDALOld.Models;
using BlogDALOld.Models.DTO;
using Google.Apis.Auth;
using System.Threading.Tasks;

namespace BlogBLOld
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> InternalAuthenticateUser(AuthenticationRequest model);
        Task<AuthenticationResponse> ExternalAuthenticateUser(ExternalAuthDTO model);
    }
}