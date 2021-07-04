using BlogDAL.Models;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateUser(AuthenticationRequest model);
    }
}