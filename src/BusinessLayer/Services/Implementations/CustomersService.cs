using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VehicleShop.BusinessLayer.Services.Interfaces;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.BusinessLayer.Services.Implementations
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<IdentityResult> RegisterCustomerAsync(string email, string password)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            ApplicationUser user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            var custromer = new Customer
            {
                Balance = 10000
            };

            return await _customersRepository.AddCustomerAppUserPairAsync(custromer, user);
        }

        public Task<Customer> GetByAppUserIdAsync(string userId)
        {
            return _customersRepository.GetCustomerByUserIdAsync(userId);
        }

        public Task<Customer> GetByAppUserNameAsync(string userName)
        {
            return _customersRepository.GetCustomerByUserNameAsync(userName);
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            return  _customersRepository.UpdateAsync(customer);
        }
    }
}