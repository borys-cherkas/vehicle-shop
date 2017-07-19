using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.Repositories.Interfaces
{
    public interface IDistributorsRepository
    {
        Task<IList<Distributor>> GetDistributorsAsync(
            Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null);
    }
}