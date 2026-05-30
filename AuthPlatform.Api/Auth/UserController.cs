using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthPlatform.Api.Auth
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IAuthUserService _userService;

        public UserController(IAuthUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{authUserId:int}")]
        public async Task<IActionResult> GetById(int authUserId)
        {
            var result = await _userService.GetByIdAsync(authUserId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto request)
        {
            var result = await _userService.CreateAsync(request);

            return Ok(result);
        }

        [HttpPut("{authUserId:int}")]
        public async Task<IActionResult> Update(
            int authUserId,
            CreateUserDto request)
        {
            request.AuthUserId = authUserId;

            var result = await _userService.UpdateAsync(request);

            return Ok(result);
        }
    }
}