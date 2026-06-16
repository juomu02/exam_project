
using Microsoft.AspNetCore.Mvc;
using App.Services;
using App.API.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateAsync([FromBody]
            CreateUserRequest createUser)
        {
            try
            {
                var userId = await userService.AddAsync(createUser.UserName,
                    createUser.Email, createUser.Password);
                return Created("/", userId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Endpointas skirtas testavimui
        [Authorize(Policy = "adminOnly")]
        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await userService.GetAsync(id);
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);
        }


        [Authorize(Policy = "adminOnly")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody]
            ChangePasswordRequest changePassword)
        {
            var result = await userService.ChangePasswordAsync(changePassword.UserId,
                changePassword.CurrentPassword, changePassword.NewPassword);
            return Ok(result);
        }


        [Authorize(Policy = "adminOnly")]
        [HttpPost("change-email")]
        public async Task<IActionResult> ChangeEmailAsync([FromBody]
            ChangeEmailRequest changeEmail)
        {
            var result = await userService.ChangeEmailAsync(changeEmail.UserId,
                changeEmail.Email);
            return Ok(result);
        }


        [Authorize(Policy = "adminOnly")]
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await userService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}