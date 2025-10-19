using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Services.Entities;

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
            Status = ServiceOrderStatus.Received;
            Services = new List<ServiceOrderService> { };
            Materials = new List<ServiceOrderMaterial> { };
            VehicleId = vehicleId;

            if (id.HasValue) Id = id.Value;
        }

        public ServiceOrder(ServiceOrder serviceOrder, Guid? id)
        {
            VehicleId = serviceOrder.VehicleId;
            Services = serviceOrder.Services;
            Materials = serviceOrder.Materials;
            Status = serviceOrder.Status;

            if (id.HasValue) Id = id.Value;
        }
    }
}