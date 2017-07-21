using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleShop.DataLayer.DbContext;
using VehicleShop.DataLayer.Entities;
using VehicleShop.DataLayer.Repositories.Interfaces;

namespace VehicleShop.DataLayer.Repositories.Implementations
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IVehiclesRepository _vehiclesRepository;

        public SalesRepository(ApplicationDbContext dbContext,
            IVehiclesRepository vehiclesRepository)
        {
            _dbContext = dbContext;
            _vehiclesRepository = vehiclesRepository;
        }

        public async Task<Transaction> BuyCarAsync(int vehicleId, int customerId)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var vehicle = await _vehiclesRepository.GetVehicleAsync(vehicleId);

                    await TransferMoneyAsync(customerId, vehicle.DistributorId.Value, vehicle.Cost);


                    var transactionEntity = new Transaction()
                    {
                        DistributorId = vehicle.DistributorId.Value,
                        CustomerId = customerId,
                        VehicleId = vehicle.Id,
                        Amount = vehicle.Cost,
                        TransactionTime = DateTime.UtcNow
                    };
                    var newTransaction = await _dbContext.Transactions.AddAsync(transactionEntity);

                    vehicle.DistributorId = null;
                    vehicle.CustomerId = customerId;

                    _dbContext.Vehicles.Update(vehicle);

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    return newTransaction.Entity;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public async Task TransferMoneyAsync(int customerId, int distributorId, double amount)
        {
            Customer customer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId);
            Distributor distributor = await _dbContext.Distributors.SingleOrDefaultAsync(x => x.Id == distributorId);

            try
            {
                customer.Balance -= amount;
                distributor.Balance += amount;

                _dbContext.Customers.Update(customer);
                _dbContext.Distributors.Update(distributor);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODO: log here
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}