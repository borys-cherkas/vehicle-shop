namespace VehicleShop.DataLayer.Entities
{
    public class Distributor
    {
        public int Id { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }

        public string ContactPhone { get; set; }

        public decimal Balance { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}