using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.DataLayer.DbContext;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Extensions;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.DataLayer.Repositories.Implementations
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Transaction> _transactions;

        public TransactionsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _transactions = dbContext.Transactions;
        }

        public async Task<IList<Transaction>> GetAllAsync(Func<IQueryable<Transaction>, IQueryable<Transaction>> queryFunc = null)
        {
            var query = _transactions
                .ProcessQuery(queryFunc);

            return await query.ToListAsync();
        }

        public Task<Transaction> GetByIdAsync(int id, Func<IQueryable<Transaction>, IQueryable<Transaction>> queryFunc = null)
        {
            var query = _transactions
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            var newTransaction = await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            return newTransaction.Entity;
        }
    }
}
