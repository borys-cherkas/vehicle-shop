using System.ComponentModel.DataAnnotations;
using VehicleShop.DataLayer.Entities;

namespace VehicleShop.Models.Distributor
{
    public class EditVehicleViewModel
    {
        public EditVehicleViewModel()
        {
            
        }

        public EditVehicleViewModel(Vehicle vehicle)
        {
            Id = vehicle.Id;
            Name = vehicle.Name;
            Description = vehicle.Description;
            IsSelling = vehicle.IsSelling;
            Cost = vehicle.Cost;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsSelling { get; set; }

        [Range(0D, 99999999D)]
        public double Cost { get; set; }
    }
}
