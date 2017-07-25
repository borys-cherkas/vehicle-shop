using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.DataLayer.DbContext;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Extensions;
using VehicleShop.DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VehicleShop.DataLayer.Constants;

namespace VehicleShop.DataLayer.Repositories.Implementations
{
    public class DistributorsRepository : IDistributorsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Distributor> _distributors;

        public DistributorsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _distributors = dbContext.Distributors;
        }

        public async Task<IList<Distributor>> GetDistributorsAsync(Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null)
        {
            var query = _distributors
                .ProcessQuery(queryFunc);

            return await query.ToListAsync();
        }

        public Task<Distributor> GetDistributorByUserIdAsync(string userId, Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null)
        {
            var query = _distributors
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.User.Id == userId);
        }

        public Task<Distributor> GetDistributorByAppUserNameAsync(string username, Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null)
        {
            var query = _distributors
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.User.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public Task<Distributor> GetDistributorByIdAsync(int id, Func<IQueryable<Distributor>, IQueryable<Distributor>> queryFunc = null)
        {
            var query = _distributors
                .ProcessQuery(queryFunc);

            return query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IdentityResult> CreateDistributorWithUserAsync(ApplicationUser appUser, Distributor distributor)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                IdentityResult res;
                using (var userStore = new UserStore<ApplicationUser>(_dbContext))
                {
                    res = await userStore.CreateAsync(appUser);

                    if (res.Succeeded)
                    {
                        await userStore.AddToRoleAsync(appUser, RolesConstants.Distributor);

                        distributor.UserId = appUser.Id;
                        await _dbContext.Distributors.AddAsync(distributor);
                        await _dbContext.SaveChangesAsync();

                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }

                return res;
            }
        }

        public Task UpdateAsync(Distributor distributor)
        {
            _dbContext.Distributors.Update(distributor);
            return _dbContext.SaveChangesAsync();
        }
    }
}
