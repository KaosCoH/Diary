using Diary.Data.Models.Requests;
using Diary.Data.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diary.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost(Name = "Register")]
        public IActionResult Register([FromBody] RegisterUserRequest registerUserRequest)
        {
            _usersService.Register(registerUserRequest);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}