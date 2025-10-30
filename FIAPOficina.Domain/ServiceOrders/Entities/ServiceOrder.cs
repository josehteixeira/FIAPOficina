namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public class ServiceOrder
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public List<ServiceOrderService> Services { get; set; }
        public List<ServiceOrderMaterial> Materials { get; set; }
        public ServiceOrderStatus Status { get; set; }

        public ServiceOrder(Guid vehicleId, Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
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