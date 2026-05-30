using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Application.Expenditures.DTOs;

namespace AuthPlatform.Application.Expenditures.Interfaces
{
    public interface IExpenditureInvoiceService
    {
        Task<ApiResponse<List<ExpenditureInvoiceDto>>> GetAllAsync();

        Task<ApiResponse<ExpenditureInvoiceDto>> GetByIdAsync(int expenditureInvoiceId);

        Task<ApiResponse<string>> CreateAsync(ExpenditureInvoiceDto request);

        Task<ApiResponse<string>> UpdateAsync(ExpenditureInvoiceDto request);

        Task<ApiResponse<string>> DeleteAsync(int expenditureInvoiceId);
    }
}
