using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VehicleShop.DataLayer.Constants;

namespace VehicleShop.DataLayer.DbContext
{
    public static class DatabaseInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            AddRoles(dbContext);
        }

        private static void AddRoles(ApplicationDbContext dbContext)
        {
            if (!dbContext.Roles.Any(x => x.Name == RolesConstants.Distributor))
            {
                dbContext.Roles.Add(new IdentityRole(RolesConstants.Distributor));
                dbContext.SaveChanges();
            }

            if (!dbContext.Roles.Any(x => x.Name == RolesConstants.Customer))
            {
                dbContext.Roles.Add(new IdentityRole(RolesConstants.Customer));
                dbContext.SaveChanges();
            }
        }
    }
}