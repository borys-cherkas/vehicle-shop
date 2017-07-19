using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleShop.DataLayer.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        [Range(0D, Double.MaxValue)]
        public double Balance { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
