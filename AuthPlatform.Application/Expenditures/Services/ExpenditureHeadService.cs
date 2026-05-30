using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Application.Expenditures.DTOs;
using AuthPlatform.Application.Expenditures.Interfaces;
using AuthPlatform.Domain.Expenditures.Entities;
using AuthPlatform.Domain.Expenditures.Interfaces;
using AutoMapper;

namespace AuthPlatform.Application.Expenditures.Services
{
    public class ExpenditureHeadService : IExpenditureHeadService
    {
        private readonly IExpenditureHeadRepository _repository;
        private readonly IMapper _mapper;

        public ExpenditureHeadService(
            IExpenditureHeadRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ExpenditureHeadDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            return new ApiResponse<List<ExpenditureHeadDto>>
            {
                Success = true,
                Message = "Expenditure heads loaded successfully.",
                Data = _mapper.Map<List<ExpenditureHeadDto>>(entities)
            };
        }

        public async Task<ApiResponse<ExpenditureHeadDto>> GetByIdAsync(
            int expenditureHeadId)
        {
            var entity = await _repository.GetByIdAsync(expenditureHeadId);

            if (entity == null)
            {
                return new ApiResponse<ExpenditureHeadDto>
                {
                    Success = false,
                    Message = "Expenditure head not found."
                };
            }

            return new ApiResponse<ExpenditureHeadDto>
            {
                Success = true,
                Message = "Expenditure head loaded successfully.",
                Data = _mapper.Map<ExpenditureHeadDto>(entity)
            };
        }

        public async Task<ApiResponse<string>> CreateAsync(
            ExpenditureHeadDto request)
        {
            var entity = new ExpenditureHead
            {
                ExpenditureHeadName = request.ExpenditureHeadName,
                Description = request.Description,

                IsActive = true,

                RecordCreatedDate = DateTime.UtcNow,
                RecordCreatedBy = request.RecordCreatedBy
            };

            await _repository.AddAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Expenditure head created successfully."
            };
        }

        public async Task<ApiResponse<string>> UpdateAsync(
            ExpenditureHeadDto request)
        {
            var entity = await _repository.GetByIdAsync(
                request.ExpenditureHeadId);

            if (entity == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Expenditure head not found."
                };
            }

            entity.ExpenditureHeadName = request.ExpenditureHeadName;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;

            entity.RecordUpdatedDate = DateTime.UtcNow;
            entity.RecordUpdatedBy = request.RecordUpdatedBy;

            await _repository.UpdateAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Expenditure head updated successfully."
            };
        }

        public async Task<ApiResponse<string>> DeleteAsync(
            int expenditureHeadId)
        {
            var entity = await _repository.GetByIdAsync(expenditureHeadId);

            if (entity == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Expenditure head not found."
                };
            }

            await _repository.DeleteAsync(entity);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Expenditure head deleted successfully."
            };
        }
    }
}