using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleShop.DataLayer.Constants;
using VehicleShop.DataLayer.DbContext;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Extensions;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.DataLayer.Repositories.Implementations
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Customer> _customers;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomersRepository(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _customers = dbContext.Customers;
            _userManager = userManager;
        }

        public Task<Customer> GetCustomerAsync(int id, Func<IQueryable<Customer>, IQueryable<Customer>> queryFunc = null)
        {
            var query = _customers
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<Customer> GetCustomerByUserNameAsync(string userName, Func<IQueryable<Customer>, IQueryable<Customer>> queryFunc = null)
        {
            var query = _customers
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.User.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
        }

        public Task<Customer> GetCustomerByUserIdAsync(string userId, Func<IQueryable<Customer>, IQueryable<Customer>> queryFunc = null)
        {
            var query = _customers
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IdentityResult> AddCustomerAppUserPairAsync(Customer customer, ApplicationUser user)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                    
                    await _userManager.AddToRoleAsync(user, RolesConstants.Customer);

                    customer.UserId = user.Id;
                    await _dbContext.Customers.AddAsync(customer);
                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Task UpdateAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            return _dbContext.SaveChangesAsync();
        }
    }
}
