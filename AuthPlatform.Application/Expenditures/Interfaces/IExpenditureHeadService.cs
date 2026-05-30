using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Application.Expenditures.DTOs;

namespace AuthPlatform.Application.Expenditures.Interfaces
{
    public interface IExpenditureHeadService
    {
        Task<ApiResponse<List<ExpenditureHeadDto>>> GetAllAsync();

        Task<ApiResponse<ExpenditureHeadDto>> GetByIdAsync(int expenditureHeadId);

        Task<ApiResponse<string>> CreateAsync(ExpenditureHeadDto request);

        Task<ApiResponse<string>> UpdateAsync(ExpenditureHeadDto request);

        Task<ApiResponse<string>> DeleteAsync(int expenditureHeadId);
    }
}
