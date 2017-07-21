using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.BusinessLayer.Services.Implementations
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsService(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public Task<IList<Transaction>> GetTransactionsAsync()
        {
            return _transactionsRepository.GetAllAsync();
        }

        public Task<IList<Transaction>> GetTransactionsByDistributorIdAsync(int distributorId)
        {
            return _transactionsRepository.GetAllAsync(query => query
            .Where(t => t.DistributorId == distributorId)
            .Include(t => t.Vehicle)
            .Include(t => t.Customer));
        }

        public Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return _transactionsRepository.GetByIdAsync(id);
        }
    }
}
