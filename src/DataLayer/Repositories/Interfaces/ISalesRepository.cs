using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.Repositories.Interfaces
{
    public interface ISalesRepository
    {
        Task<Transaction> BuyCarAsync(int vehicleId, int customerId);

        Task TransferMoneyAsync(int customerId, int distributorId, double amount);
    }
}
