using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.BusinessLayer.Services.Interfaces
{
    public interface ICustomersService
    {
        Task<IdentityResult> RegisterCustomerAsync(string userName, string password);

        Task<Customer> GetByAppUserIdAsync(string userId);
        
        Task<Customer> GetByAppUserNameAsync(string userName);

        Task UpdateCustomerAsync(Customer customer);
    }
}
