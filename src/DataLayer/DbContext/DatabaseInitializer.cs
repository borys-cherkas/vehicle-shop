using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VehicleShop.DataLayer.Constants;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.DataLayer.DbContext
{
    public static class DatabaseInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var userManager =
                serviceProvider.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;

            var roleManager =
                serviceProvider.GetService(typeof(RoleManager<IdentityRole>)) as RoleManager<IdentityRole>;

            AddRoles(roleManager);
            AddAdministrators(userManager);
            AddDistributors(dbContext, userManager);
            AddVehicles(dbContext);
        }

        private static void AddRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync(RolesConstants.Administrator).Result == null)
            {
                var _ = roleManager.CreateAsync(new IdentityRole(RolesConstants.Administrator)).Result;
            }

            if (roleManager.FindByNameAsync(RolesConstants.Distributor).Result == null)
            {
                var _ = roleManager.CreateAsync(new IdentityRole(RolesConstants.Distributor)).Result;
            }

            if (roleManager.FindByNameAsync(RolesConstants.Customer).Result == null)
            {
                var _ = roleManager.CreateAsync(new IdentityRole(RolesConstants.Customer)).Result;
            }
        }

        private static void AddAdministrators(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "admin@admin.admin";
            var adminUser = userManager.FindByNameAsync(adminEmail).Result;
            if (adminUser == null)
            {
                adminUser = new ApplicationUser() { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var _ = userManager.CreateAsync(adminUser, "TestPass123!").Result;
                _ = userManager.AddToRoleAsync(adminUser, RolesConstants.Administrator).Result;
            }
        }

        private static void AddDistributors(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            string email = "distributor@test.test";
            var user = userManager.FindByNameAsync(email).Result;
            if (user == null)
            {
                user = new ApplicationUser() { UserName = email, Email = email, EmailConfirmed = true };
                var _ = userManager.CreateAsync(user, "TestPass123!").Result;
                var __ = userManager.AddToRoleAsync(user, RolesConstants.Distributor).Result;

                dbContext.Distributors.Add(new Distributor()
                {
                    UserId = user.Id,
                    Balance = 100000,
                    ZipCode = "26354",
                    ContactPhone = "+38099-312-23-54"
                });
                dbContext.SaveChanges();
            }
        }

        private static void AddVehicles(ApplicationDbContext dbContext)
        {
            if (!dbContext.Vehicles.Any())
            {
                var distributor = dbContext.Distributors.First();

                var vehicles = new List<Vehicle>
                {
                    new Vehicle()
                    {
                        DistributorId =  distributor.Id,
                        Name = "Car 1",
                        Description = "The Fastest car",
                        Cost = 5000,
                        IsSelling = true
                    },
                    new Vehicle()
                    {
                        DistributorId =  distributor.Id,
                        Name = "Car 2",
                        Description = "Default car",
                        Cost = 2500,
                        IsSelling = true
                    },
                    new Vehicle()
                    {
                        DistributorId =  distributor.Id,
                        Name = "Car 3",
                        Description = "Beautiful car",
                        Cost = 7500,
                        IsSelling = true
                    }
                };

                dbContext.Vehicles.AddRange(vehicles);
                dbContext.SaveChanges();
            }
        }
    }
}