using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.DataLayer.DbContext;
using VehicleShop.DataLayer.Entities;
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

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            var newTransaction = await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            return newTransaction.Entity;
        }
    }
}
