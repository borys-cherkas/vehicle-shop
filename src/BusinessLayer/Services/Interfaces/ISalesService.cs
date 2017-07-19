using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleShop.BusinessLayer.Services.Interfaces
{
    public interface ISalesService
    {
        Task<bool> CanUserBuyVehicleAsync(string userName, int vehicleId);

        Task BuyVehicleAsync(int vehicleId, string userName);
    }
}
