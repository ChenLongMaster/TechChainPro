using BlogBL;
using BlogDAL.Models;
using BlogDAL.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost()]
        public async Task<IActionResult> InternalAuthenticate([FromBody] AuthenticationRequest model)
        {
            AuthenticationResponse response = await _authenticationService.InternalAuthenticateUser(model);

            if (response is null)
            {
                return BadRequest(new { message = "Username Or Password Is Incorrect." });
            }

            return Ok(response);
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleAuthenticate([FromBody] ExternalAuthDTO model)
        {
            var response = await _authenticationService.GoogleAuthenticateUser(model);
            if (response is null)
            {
                return BadRequest(new { message = "Invalid External Authentication." });
            }
            return Ok(response);
        }


        [HttpPost("facebook")]
        public async Task<IActionResult> FacebookAuthenticate([FromBody] ExternalAuthDTO model)
        {
            var response = await _authenticationService.FacebookAuthenticateUser(model);
            if (response is null)
            {
                return BadRequest(new { message = "Invalid External Authentication." });
            }
            return Ok(response);
        }
    }
}
