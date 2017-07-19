using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.BusinessLayer.Services.Interfaces
{
    public interface IDistributorsService
    {
        Task<IList<Distributor>> GetDistributorsAsync();
    }
}
