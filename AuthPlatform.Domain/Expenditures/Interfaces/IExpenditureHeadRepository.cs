using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthPlatform.Domain.Expenditures.Entities;

namespace AuthPlatform.Domain.Expenditures.Interfaces
{
    public interface IExpenditureHeadRepository
    {
        Task<ExpenditureHead?> GetByIdAsync(int id);
        Task<List<ExpenditureHead>> GetAllAsync();
        Task AddAsync(ExpenditureHead entity);
        Task UpdateAsync(ExpenditureHead entity);
        Task DeleteAsync(ExpenditureHead entity);
    }
}
