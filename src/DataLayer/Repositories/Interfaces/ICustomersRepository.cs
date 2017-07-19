using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.Repositories.Interfaces
{
    public interface ICustomersRepository
    {

        Task<Customer> GetCustomerAsync(int id, Func<IQueryable<Customer>, IQueryable<Customer>> queryFunc = null);

        Task<Customer> GetCustomerByUserIdAsync(string userId, Func<IQueryable<Customer>, IQueryable<Customer>> queryFunc = null);

        Task<Customer> GetCustomerByUserNameAsync(string userName, Func<IQueryable<Customer>, IQueryable<Customer>> queryFunc = null);

        Task<IdentityResult> AddCustomerAppUserPairAsync(Customer custromer, ApplicationUser user);

        Task UpdateAsync(Customer customer);
    }
}
