using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Application.Expenditures.DTOs;
using AuthPlatform.Application.Expenditures.Interfaces;
using AuthPlatform.Domain.Expenditures.Entities;
using AuthPlatform.Domain.Expenditures.Interfaces;
using AutoMapper;

namespace AuthPlatform.Application.Expenditures.Services
{
    public class ExpenditureInvoiceDetailService : IExpenditureInvoiceDetailService
    {
        private readonly IExpenditureInvoiceDetailRepository _repository;
        private readonly IMapper _mapper;

        public ExpenditureInvoiceDetailService(
            IExpenditureInvoiceDetailRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ExpenditureInvoiceDetailDto>>> GetByInvoiceIdAsync(int expenditureInvoiceId)
        {
            var entities = await _repository.GetByInvoiceIdAsync(expenditureInvoiceId);

            return new ApiResponse<List<ExpenditureInvoiceDetailDto>>
            {
                Success = true,
                Message = "Expenditure invoice details loaded successfully.",
                Data = _mapper.Map<List<ExpenditureInvoiceDetailDto>>(entities)
            };
        }

        public async Task<ApiResponse<ExpenditureInvoiceDetailDto>> GetByIdAsync(int expenditureInvoiceDetailId)
        {
            var entity = await _repository.GetByIdAsync(expenditureInvoiceDetailId);

            if (entity == null)
            {
                return new ApiResponse<ExpenditureInvoiceDetailDto>
                {
                    Success = false,
                    Message = "Expenditure invoice detail not found."
                };
            }

            return new ApiResponse<ExpenditureInvoiceDetailDto>
            {
                Success = true,
                Message = "Expenditure invoice detail loaded successfully.",
                Data = _mapper.Map<ExpenditureInvoiceDetailDto>(entity)
            };
        }

        public async Task<ApiResponse<string>> CreateAsync(ExpenditureInvoiceDetailDto request)
        {
            var entity = _mapper.Map<ExpenditureInvoiceDetail>(request);

            await _repository.AddAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Expenditure invoice detail created successfully."
            };
        }

        public async Task<ApiResponse<string>> UpdateAsync(ExpenditureInvoiceDetailDto request)
        {
            var entity = await _repository.GetByIdAsync(request.ExpenditureInvoiceDetailId);

            if (entity == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Expenditure invoice detail not found."
                };
            }

            entity.ExpenseDate = request.ExpenseDate;
            entity.ItemName = request.ItemName;
            entity.Description = request.Description;
            entity.Quantity = request.Quantity;
            entity.UnitPrice = request.UnitPrice;
            entity.LineTotal = request.Quantity * request.UnitPrice;
            entity.IsActive = true;
            entity.RecordUpdatedDate = DateTime.UtcNow;
            entity.RecordUpdatedBy = request.RecordUpdatedBy;

            await _repository.UpdateAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Expenditure invoice detail updated successfully."
            };
        }

        public async Task<ApiResponse<string>> DeleteAsync(int expenditureInvoiceDetailId)
        {
            var entity = await _repository.GetByIdAsync(expenditureInvoiceDetailId);

            if (entity == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Expenditure invoice detail not found."
                };
            }

            await _repository.DeleteAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Expenditure invoice detail deleted successfully."
            };
        }
    }
}
