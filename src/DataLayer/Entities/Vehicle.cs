using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleShop.DataLayer.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0D, Double.MaxValue)]
        public double Cost { get; set; }

        public int? DistributorId { get; set; }
        public Distributor Distributor { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
