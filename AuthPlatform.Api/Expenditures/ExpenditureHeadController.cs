using AuthPlatform.Api.Attributes;
using AuthPlatform.Application.Expenditures.DTOs;
using AuthPlatform.Application.Expenditures.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthPlatform.Api.Expenditures
{


    [ApiController]
    [Route("api/expenditure-heads")]
    [Authorize]
    public class ExpenditureHeadController : ControllerBase
    {
        private readonly IExpenditureHeadService _service;

        public ExpenditureHeadController(IExpenditureHeadService service)
        {
            _service = service;
        }

        [HttpGet]
        [HasPermission("ExpenditureHead.View")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{expenditureHeadId:int}")]
        [HasPermission("ExpenditureHead.View")]
        public async Task<IActionResult> GetById(int expenditureHeadId)
        {
            var result = await _service.GetByIdAsync(expenditureHeadId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [HasPermission("ExpenditureHead.Create")]
        public async Task<IActionResult> Create(ExpenditureHeadDto request)
        {
            var result = await _service.CreateAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{expenditureHeadId:int}")]
        [HasPermission("ExpenditureHead.Edit")]
        public async Task<IActionResult> Update(
            int expenditureHeadId,
            ExpenditureHeadDto request)
        {
            request.ExpenditureHeadId = expenditureHeadId;

            var result = await _service.UpdateAsync(request);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{expenditureHeadId:int}")]
        [HasPermission("ExpenditureHead.Delete")]
        public async Task<IActionResult> Delete(int expenditureHeadId)
        {
            var result = await _service.DeleteAsync(expenditureHeadId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
