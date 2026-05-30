using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Application.Expenditures.DTOs;

namespace AuthPlatform.Application.Expenditures.Interfaces
{
    public interface IExpenditureInvoiceDetailService
    {
        Task<ApiResponse<List<ExpenditureInvoiceDetailDto>>> GetByInvoiceIdAsync(int expenditureInvoiceId);

        Task<ApiResponse<ExpenditureInvoiceDetailDto>> GetByIdAsync(int expenditureInvoiceDetailId);

        Task<ApiResponse<string>> CreateAsync(ExpenditureInvoiceDetailDto request);

        Task<ApiResponse<string>> UpdateAsync(ExpenditureInvoiceDetailDto request);

        Task<ApiResponse<string>> DeleteAsync(int expenditureInvoiceDetailId);
    }
}
