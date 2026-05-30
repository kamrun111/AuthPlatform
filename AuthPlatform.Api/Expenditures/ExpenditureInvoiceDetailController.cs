using AuthPlatform.Api.Attributes;
using AuthPlatform.Application.Expenditures.DTOs;
using AuthPlatform.Application.Expenditures.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AuthPlatform.Api.Expenditures
{
  

 

    [ApiController]
    [Route("api/expenditure-invoice-details")]
    [Authorize]
    public class ExpenditureInvoiceDetailController : ControllerBase
    {
        private readonly IExpenditureInvoiceDetailService _service;

        public ExpenditureInvoiceDetailController(IExpenditureInvoiceDetailService service)
        {
            _service = service;
        }

        [HttpGet("by-invoice/{expenditureInvoiceId:int}")]
        [HasPermission("ExpenditureInvoiceDetail.View")]
        public async Task<IActionResult> GetByInvoiceId(int expenditureInvoiceId)
        {
            var result = await _service.GetByInvoiceIdAsync(expenditureInvoiceId);

            return Ok(result);
        }

        [HttpGet("{expenditureInvoiceDetailId:int}")]
        [HasPermission("ExpenditureInvoiceDetail.View")]
        public async Task<IActionResult> GetById(int expenditureInvoiceDetailId)
        {
            var result = await _service.GetByIdAsync(expenditureInvoiceDetailId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [HasPermission("ExpenditureInvoiceDetail.Create")]
        public async Task<IActionResult> Create(ExpenditureInvoiceDetailDto request)
        {
            var result = await _service.CreateAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{expenditureInvoiceDetailId:int}")]
        [HasPermission("ExpenditureInvoiceDetail.Edit")]
        public async Task<IActionResult> Update(
            int expenditureInvoiceDetailId,
            ExpenditureInvoiceDetailDto request)
        {
            request.ExpenditureInvoiceDetailId = expenditureInvoiceDetailId;

            var result = await _service.UpdateAsync(request);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{expenditureInvoiceDetailId:int}")]
        [HasPermission("ExpenditureInvoiceDetail.Delete")]
        public async Task<IActionResult> Delete(int expenditureInvoiceDetailId)
        {
            var result = await _service.DeleteAsync(expenditureInvoiceDetailId);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
