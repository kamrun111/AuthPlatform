using AuthPlatform.Application.Common.Responses;
using AuthPlatform.Application.Expenditures.DTOs;
using AuthPlatform.Application.Expenditures.Interfaces;
using AuthPlatform.Domain.Expenditures.Entities;
using AuthPlatform.Domain.Expenditures.Interfaces;
using AutoMapper;

namespace AuthPlatform.Application.Expenditures.Services;

public class ExpenditureInvoiceService : IExpenditureInvoiceService
{
    private readonly IExpenditureInvoiceRepository _repository;
    private readonly IMapper _mapper;

    public ExpenditureInvoiceService(IExpenditureInvoiceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenditureInvoiceDto>>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        return new ApiResponse<List<ExpenditureInvoiceDto>>
        {
            Success = true,
            Message = "Invoices loaded successfully.",
            Data = _mapper.Map<List<ExpenditureInvoiceDto>>(entities)
        };
    }

    public async Task<ApiResponse<ExpenditureInvoiceDto>> GetByIdAsync(int expenditureInvoiceId)
    {
        var entity = await _repository.GetByIdAsync(expenditureInvoiceId);

        if (entity == null)
        {
            return new ApiResponse<ExpenditureInvoiceDto>
            {
                Success = false,
                Message = "Invoice not found."
            };
        }

        return new ApiResponse<ExpenditureInvoiceDto>
        {
            Success = true,
            Message = "Invoice loaded successfully.",
            Data = _mapper.Map<ExpenditureInvoiceDto>(entity)
        };
    }

    public async Task<ApiResponse<string>> CreateAsync(ExpenditureInvoiceDto request)
    {
        var year = DateTime.Now.Year;

        var month = DateTime.Now.Month;

        var count = await _repository.GetInvoiceCountByMonthAsync(year, month);

        var invoiceNumber = $"INV-{year}{month:D2}-{(count + 1):D4}";

        var entity = new ExpenditureInvoice
        {
            InvoiceNumber = invoiceNumber,
            InvoiceDate = request.InvoiceDate,
            ExpenditureHeadId = request.ExpenditureHeadId,
            IsActive = true,
            RecordCreatedDate = DateTime.UtcNow,
            RecordCreatedBy = request.RecordCreatedBy
        };

        foreach (var item in request.Details)
        {
            var lineTotal = item.Quantity * item.UnitPrice;

            entity.Details.Add(new ExpenditureInvoiceDetail
            {
                ExpenseDate = item.ExpenseDate,
                ItemName = item.ItemName,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                LineTotal = lineTotal,
                IsActive = true,
                RecordCreatedDate = DateTime.UtcNow,
                RecordCreatedBy = request.RecordCreatedBy
            });
        }

        entity.TotalAmount = entity.Details.Sum(x => x.LineTotal);

        await _repository.AddAsync(entity);

        return new ApiResponse<string>
        {
            Success = true,
            Message = "Invoice created successfully."
        };
    }

    public async Task<ApiResponse<string>> UpdateAsync(ExpenditureInvoiceDto request)
    {
        var entity = await _repository.GetByIdAsync(request.ExpenditureInvoiceId);

        if (entity == null)
        {
            return new ApiResponse<string>
            {
                Success = false,
                Message = "Invoice not found."
            };
        }

        entity.InvoiceDate = request.InvoiceDate;
        entity.ExpenditureHeadId = request.ExpenditureHeadId;
        entity.RecordUpdatedDate = DateTime.UtcNow;
        entity.RecordUpdatedBy = request.RecordUpdatedBy;

        entity.Details.Clear();

        foreach (var item in request.Details)
        {
            var lineTotal = item.Quantity * item.UnitPrice;

            entity.Details.Add(new ExpenditureInvoiceDetail
            {
                ItemName = item.ItemName,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                LineTotal = lineTotal,
                IsActive = true,
                ExpenseDate = item.ExpenseDate,
                RecordCreatedDate = DateTime.UtcNow,
                RecordCreatedBy = request.RecordUpdatedBy
            });
        }

        entity.TotalAmount = entity.Details.Sum(x => x.LineTotal);

        await _repository.UpdateAsync(entity);

        return new ApiResponse<string>
        {
            Success = true,
            Message = "Invoice updated successfully."
        };
    }

    public async Task<ApiResponse<string>> DeleteAsync(int expenditureInvoiceId)
    {
        var entity = await _repository.GetByIdAsync(expenditureInvoiceId);

        if (entity == null)
        {
            return new ApiResponse<string>
            {
                Success = false,
                Message = "Invoice not found."
            };
        }

        await _repository.DeleteAsync(entity);

        return new ApiResponse<string>
        {
            Success = true,
            Message = "Invoice deleted successfully."
        };
    }
}