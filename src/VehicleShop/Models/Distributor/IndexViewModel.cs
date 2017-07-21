using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.Models.Distributor
{
    public class IndexViewModel
    {
        public DataLayer.Entities.Distributor Distributor { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }
        
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
