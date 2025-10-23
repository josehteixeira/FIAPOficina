namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class ServiceOrderServices
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Services Service { get; set; } = null!;
        public Guid ServiceOrderId { get; set; }
        public ServiceOrders ServiceOrder { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}