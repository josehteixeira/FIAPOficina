namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class Services
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public ICollection<ServiceOrderServices> ServiceOrderServices { get; set; } = null!;
    }
}