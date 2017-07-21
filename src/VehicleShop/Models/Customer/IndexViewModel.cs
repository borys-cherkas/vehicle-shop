using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.Models.Customer
{
    public class IndexViewModel
    {
        public DataLayer.Entities.Customer Customer { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }
    }
}
