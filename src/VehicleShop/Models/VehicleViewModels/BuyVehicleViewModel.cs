using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleShop.Models.VehicleViewModels
{
    public class BuyVehicleViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
