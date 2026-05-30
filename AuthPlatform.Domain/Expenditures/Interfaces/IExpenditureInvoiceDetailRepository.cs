using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Expenditures.Entities;

namespace AuthPlatform.Domain.Expenditures.Interfaces
{
    public interface IExpenditureInvoiceDetailRepository
    {
        Task<List<ExpenditureInvoiceDetail>> GetByInvoiceIdAsync(int expenditureInvoiceId);

        Task<ExpenditureInvoiceDetail?> GetByIdAsync(int expenditureInvoiceDetailId);

        Task AddAsync(ExpenditureInvoiceDetail entity);

        Task UpdateAsync(ExpenditureInvoiceDetail entity);

        Task DeleteAsync(ExpenditureInvoiceDetail entity);
    }
}
