using Microsoft.AspNetCore.Identity;
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

        Task<Distributor> GetDistributorByIdAsync(int id,
            Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null);

        Task<Distributor> GetDistributorByUserIdAsync(string userId,
            Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null);

        Task<Distributor> GetDistributorByAppUserNameAsync(string username,
            Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null);

        Task<IdentityResult> CreateDistributorWithUserAsync(ApplicationUser appUser, Distributor distributor);

        Task UpdateAsync(Distributor distributor);
    }
}