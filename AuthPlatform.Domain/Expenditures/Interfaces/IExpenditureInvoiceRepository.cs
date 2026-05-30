using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Expenditures.Entities;

namespace AuthPlatform.Domain.Expenditures.Interfaces
{
    public interface IExpenditureInvoiceRepository
    {
        Task<List<ExpenditureInvoice>> GetAllAsync();

        Task<ExpenditureInvoice?> GetByIdAsync(int expenditureInvoiceId);

        Task AddAsync(ExpenditureInvoice entity);

        Task UpdateAsync(ExpenditureInvoice entity);

        Task DeleteAsync(ExpenditureInvoice entity);

        Task<int> GetInvoiceCountByMonthAsync(int year, int month);
    }
}
