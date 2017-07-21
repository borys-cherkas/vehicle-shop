using System.ComponentModel.DataAnnotations;

namespace VehicleShop.Models.ManageViewModels
{
    public class ReplenishAccountViewModel
    {
        [Required]
        [Range(0.1D, 3000D)]
        public double Amount { get; set; }
    }
}
