using Application.Contracts.Services;
using Application.DTOs.User;
using Domain.Entities;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace UserAndOrderTask.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService userService ,ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
          
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var  user = await _userService.RegisterUserAsync(request);
            return Ok(user);
        }

    }
}
