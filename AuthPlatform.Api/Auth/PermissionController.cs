using AuthPlatform.Application.Auth.DTOs;
using AuthPlatform.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace AuthPlatform.Api.Auth
{
  

    [ApiController]
    [Route("api/permissions")]
    [Authorize]
    public class PermissionController : ControllerBase
    {
        private readonly IAuthPermissionService _permissionService;

        public PermissionController(IAuthPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _permissionService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{authPermissionId:int}")]
        public async Task<IActionResult> GetById(int authPermissionId)
        {
            var result = await _permissionService.GetByIdAsync(authPermissionId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthPermissionDto request)
        {
            var result = await _permissionService.CreateAsync(request);

            return Ok(result);
        }

        [HttpPut("{authPermissionId:int}")]
        public async Task<IActionResult> Update(
            int authPermissionId,
            AuthPermissionDto request)
        {
            request.AuthPermissionId = authPermissionId;

            var result = await _permissionService.UpdateAsync(request);

            return Ok(result);
        }

        [HttpDelete("{authPermissionId:int}")]
        public async Task<IActionResult> Delete(int authPermissionId)
        {
            var result = await _permissionService.DeleteAsync(authPermissionId);

            return Ok(result);
        }

        [HttpPost("assign-to-user")]
        public async Task<IActionResult> AssignToUser(AuthUserPermissionDto request)
        {
            var result = await _permissionService.AssignPermissionToUserAsync(request);

            return Ok(result);
        }

        [HttpPost("assign-to-group")]
        public async Task<IActionResult> AssignToGroup(AuthGroupPermissionDto request)
        {
            var result = await _permissionService.AssignPermissionToGroupAsync(request);

            return Ok(result);
        }

        [HttpGet("user/{authUserId:int}")]
        public async Task<IActionResult> GetUserPermissions(int authUserId)
        {
            var result = await _permissionService.GetUserPermissionCodesAsync(authUserId);

            return Ok(result);
        }

        [HttpGet("group/{authGroupId:int}")]
        public async Task<IActionResult> GetGroupPermissions(int authGroupId)
        {
            var result = await _permissionService.GetGroupPermissionCodesAsync(authGroupId);

            return Ok(result);
        }


        [HttpPost("save-group-permissions")]
        public async Task<IActionResult> SaveGroupPermissions(SaveGroupPermissionsDto request)
        {
            var result = await _permissionService
                .SaveGroupPermissionsAsync(request);

            return Ok(result);
        }

        [HttpPost("save-user-permissions")]
        public async Task<IActionResult> SaveUserPermissions(SaveUserPermissionsDto request)
        {
            var result = await _permissionService
                .SaveUserPermissionsAsync(request);

            return Ok(result);
        }
    }
}
