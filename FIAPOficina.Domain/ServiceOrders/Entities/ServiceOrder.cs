namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public class ServiceOrder
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public List<ServiceOrderService> Services { get; set; }
        public List<ServiceOrderMaterial> Materials { get; set; }
        public ServiceOrderStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public DateTime? FinishedOn { get; set; }

        public ServiceOrder(Guid vehicleId, DateTime? createdOn = null, DateTime? approvedOn = null, DateTime? finishedOn = null, Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
            CreatedOn = createdOn.HasValue ? createdOn.Value : DateTime.UtcNow;
            ApprovedOn = approvedOn;
            FinishedOn = finishedOn;
            VehicleId = vehicleId;
            Services = new List<ServiceOrderService> { };
            Materials = new List<ServiceOrderMaterial> { };
        }

        public ServiceOrder(ServiceOrder serviceOrder, Guid id)
        {
            Id = id;
            VehicleId = serviceOrder.VehicleId;
            Services = serviceOrder.Services;
            Materials = serviceOrder.Materials;
            Status = serviceOrder.Status;
        }
    }
}