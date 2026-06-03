using AuthPlatform.Application.Expenditures.DTOs;
using AuthPlatform.Application.Expenditures.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthPlatform.Api.Expenditures
{


    [ApiController]
    [Route("api/expenditure-invoices")]
    [Authorize]
    public class ExpenditureInvoiceController : ControllerBase
    {
        private readonly IExpenditureInvoiceService _service;

        public ExpenditureInvoiceController(IExpenditureInvoiceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenditureInvoiceDto request)
        {
            var result = await _service.CreateAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,ExpenditureInvoiceDto request)
        {
            if (id != request.ExpenditureInvoiceId)
            {
                return BadRequest();
            }

            var result = await _service.UpdateAsync(request);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
