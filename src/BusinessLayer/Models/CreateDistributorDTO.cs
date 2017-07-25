using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleShop.BusinessLayer.Models
{
    public class CreateDistributorDTO
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }

        public string ContactPhone { get; set; }

    }
}
