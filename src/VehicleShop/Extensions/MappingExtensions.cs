using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleShop.BusinessLayer.Models;
using VehicleShop.Models.Admin;

namespace VehicleShop.Extensions
{
    public static class MappingExtensions
    {
        public static CreateDistributorDTO MapToCreateDistributorDTO(this CreateDistributorViewModel model)
        {
            return new CreateDistributorDTO
            {
                Email = model.Email,
                Password = model.Password,
                ZipCode = model.ZipCode,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                ContactPhone = model.ContactPhone
            };
        }
    }
}
