using System;

namespace VehicleShop.DataLayer.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public DateTime TransactionTime { get; set; }

        public double Amount { get; set; }

        public int DistributorId { get; set; }
        public Distributor Distributor { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
