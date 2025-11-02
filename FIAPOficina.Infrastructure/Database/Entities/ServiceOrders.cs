namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class ServiceOrders
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime? FinishedOn { get; set; }
        public Vehicles Vehicle { get; set; } = null!;
        public ICollection<ServiceOrderServices> Services { get; set; } = null!;
        public ICollection<ServiceOrderMaterials> Materials { get; set; } = null!;
    }
}