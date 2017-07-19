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
    }
}
