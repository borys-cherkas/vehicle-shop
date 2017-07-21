using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<IList<Transaction>> GetAllAsync(Func<IQueryable<Transaction>, IQueryable<Transaction>> queryFunc = null);

        Task<Transaction> GetByIdAsync(int id, Func<IQueryable<Transaction>, IQueryable<Transaction>> queryFunc = null);

        Task<Transaction> AddTransactionAsync(Transaction transaction);
    }
}
