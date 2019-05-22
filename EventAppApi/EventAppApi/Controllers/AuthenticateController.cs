using Microsoft.AspNetCore.Mvc;
using EventAppApi.Models;
using EventAppApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace EventAppApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Account userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("getCurrentAccount")]
        public IActionResult GetCurrentAccount([FromHeader]string token)
        {
            var user = _userService.GetCurrentAccount(token);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("getAccountEvents")]
        public IActionResult GetAccountEvents([FromHeader]string token)
        {
            var events = _userService.GetEvents(token);
            return Ok(events);
        }

        [AllowAnonymous]
        [HttpPost("getEventsInYourCity")]
        public IActionResult GetEventsInYourCity([FromHeader]string token)
        {
            var events = _userService.GetEventsInYourCity(token);
            return Ok(events);
        }
    }

}
