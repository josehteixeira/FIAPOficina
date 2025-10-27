namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class Materials
    {
         public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public ICollection<ServiceOrderMaterials> ServiceOrderMaterials { get; set; } = null!;
    }
}