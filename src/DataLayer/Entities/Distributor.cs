using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleShop.DataLayer.Entities
{
    public class Distributor
    {
        public int Id { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }

        public string ContactPhone { get; set; }

        [Range(0D, Double.MaxValue)]
        public double Balance { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}