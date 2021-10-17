using TechchainBL;
using TechchainDAL.Models;
using TechchainDAL.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TechchainProject.Controllers
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

        [HttpPost("external")]
        public async Task<IActionResult> ExternalAuthenticate([FromBody] ExternalAuthDTO model)
        {
            var response = await _authenticationService.ExternalAuthenticateUser(model);
            if (response is null)
            {
                return BadRequest(new { message = "Invalid External Authentication." });
            }
            return Ok(response);
        }

    }
}
