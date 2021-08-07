using BlogBL;
using BlogDAL.Models;
using BlogProject.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [Authorize]
        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            List<User> result = await _userService.GetAllUserAsync();

            return result;
        }

        [Authorize]
        [Route("{id}")]
        [HttpGet]
        public async Task<User> GetUsersById(Guid id)
        {
            User result = await _userService.GetUserByIdAsync(id);

            return result;
        }

        
    }
}
