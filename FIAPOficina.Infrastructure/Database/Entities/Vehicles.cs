namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class Vehicles
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Plate { get; set; }
        public string Color { get; set; }
        public Guid ClientId { get; set; }
        public Clients Client { get; set; }
        public ICollection<ServiceOrders> ServiceOrders { get; set; }
    }
}