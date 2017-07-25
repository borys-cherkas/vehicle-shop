using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleShop.BusinessLayer.Models;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.BusinessLayer.Services.Interfaces
{
    public interface IDistributorsService
    {
        Task<IList<Distributor>> GetDistributorsAsync();

        Task<Distributor> GetDistributorByIdAsync(int id);

        Task<Distributor> GetDistributorByUserIdAsync(string modelUserId);

        Task<Distributor> GetDistributorByUserNameAsync(string username);

        Task<IdentityResult> CreateDistributorWithUserAsync(CreateDistributorDTO dto);

        Task UpdateDistributorAsync(Distributor distributor);
    }
}
