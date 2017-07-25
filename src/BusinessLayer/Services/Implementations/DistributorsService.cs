using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.BusinessLayer.Models;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VehicleShop.DataLayer.DbContext;

namespace VehicleShop.BusinessLayer.Services.Implementations
{
    public class DistributorsService : IDistributorsService
    {
        private readonly IDistributorsRepository _distributorsRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DistributorsService(IDistributorsRepository distributorsRepository)
        {
            _distributorsRepository = distributorsRepository;
        }

        public Task<IList<Distributor>> GetDistributorsAsync()
        {
            return _distributorsRepository.GetDistributorsAsync(q => q
                .Include(d => d.Vehicles)
                .Include(d => d.User));
        }

        public Task<Distributor> GetDistributorByIdAsync(int id)
        {
            return _distributorsRepository.GetDistributorByIdAsync(id,
                x => x.Include(d => d.User));
        }

        public Task<Distributor> GetDistributorByUserIdAsync(string userId)
        {
            return _distributorsRepository.GetDistributorByUserIdAsync(userId);
        }

        public Task<Distributor> GetDistributorByUserNameAsync(string username)
        {
            return _distributorsRepository.GetDistributorByAppUserNameAsync(username);
        }

        public Task<IdentityResult> CreateDistributorWithUserAsync(CreateDistributorDTO dto)
        {
            var appUser = new ApplicationUser()
            {
                UserName = dto.Email,
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpper(),
                NormalizedUserName = dto.Email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = hasher.HashPassword(appUser, dto.Password);

            var distributor = new Distributor()
            {
                Balance = 1000,
                ContactPhone = dto.ContactPhone,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                ZipCode = dto.ZipCode
            };

            return _distributorsRepository.CreateDistributorWithUserAsync(appUser, distributor);
        }

        public Task UpdateDistributorAsync(Distributor distributor)
        {
            return _distributorsRepository.UpdateAsync(distributor);
        }
    }
}