namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class ServiceOrderMaterials
    {
        public Guid Id { get; set; }
        public Guid MaterialId { get; set; }
        public Materials Material { get; set; } = null!;
        public Guid ServiceOrderId { get; set; }
        public ServiceOrders ServiceOrder { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}