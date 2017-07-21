using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.BusinessLayer.Services.Interfaces
{
    public interface ITransactionsService
    {
        Task<IList<Transaction>> GetTransactionsAsync();

        Task<IList<Transaction>> GetTransactionsByDistributorIdAsync(int distributorId);

        Task<Transaction> GetTransactionByIdAsync(int id);
    }
}
