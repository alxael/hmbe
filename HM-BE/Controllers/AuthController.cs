using HM.Data.DataTransferObjects;
using HM.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HM.WebAPI.Controllers
{
    [CustomAuthorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        #region Fields
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForCreateDto userDto)
        {
            if (!ModelState.IsValid || userDto == null)
                return BadRequest("Given data is null or invalid");


            var result = await _userService.CreateUserAsync(userDto);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (var error in result.Errors)
                    dictionary.AddModelError(error.Code, error.Description);

                return BadRequest(dictionary);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserForLoginDto userDto)
        {
            if (!ModelState.IsValid || userDto == null)
                return BadRequest("Given data is null or invalid");

            switch(await _userService.AttemptLoginAsync(userDto))
            {
                case IUserService.LoginStatus.WrongEmail:
                    return BadRequest("Wrong email/username");
                case IUserService.LoginStatus.WrongPassword:
                    return BadRequest("Wrong password");
                case IUserService.LoginStatus.Lockout:
                    return BadRequest("Lockout enabled, please try again later");

            }

            var jwtToken = await _userService.GetJwtTokenAsync(userDto);
            return Ok(new { Token = jwtToken });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }


    }
}


