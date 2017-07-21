using System.ComponentModel.DataAnnotations;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.Models.Distributor
{
    public class ChangeVehicleSalesStateViewModel
    {
        [Required]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        [Required]
        public bool NewSalesState { get; set; }
    }
}
