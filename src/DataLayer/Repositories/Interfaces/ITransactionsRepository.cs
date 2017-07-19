using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.Repositories.Interfaces
{
    public interface ITransactionsRepository
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
    }
}
