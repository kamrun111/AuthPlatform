using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthPlatform.Api.Auth
{
    [ApiController]
    [Route("api/groups")]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _groupService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{authGroupId:int}")]
        public async Task<IActionResult> GetById(int authGroupId)
        {
            var result = await _groupService.GetByIdAsync(authGroupId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthGroupDto request)
        {
            var result = await _groupService.CreateAsync(request);

            return Ok(result);
        }

        [HttpPut("{authGroupId:int}")]
        public async Task<IActionResult> Update(int authGroupId, AuthGroupDto request)
        {
            request.AuthGroupId = authGroupId;

            var result = await _groupService.UpdateAsync(request);

            return Ok(result);
        }

        [HttpDelete("{authGroupId:int}")]
        public async Task<IActionResult> Delete(int authGroupId)
        {
            var result = await _groupService.DeleteAsync(authGroupId);

            return Ok(result);
        }
    }
}