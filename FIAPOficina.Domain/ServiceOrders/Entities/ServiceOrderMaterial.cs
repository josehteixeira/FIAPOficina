using FIAPOficina.Domain.ServiceOrders.Utils;

namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public class ServiceOrderMaterial
    {
        public Guid Id { get; private set; }
        public Guid MaterialId { get; private set; }
        public Guid ServiceOrderId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public decimal TotalValue { get => Quantity * UnitValue; }

        public ServiceOrderMaterial(Guid materialId, Guid serviceOrderId, int quantity, decimal unitValue, Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
            Quantity = ServiceOrderUtils.ValidQuantity(quantity);
            UnitValue = ServiceOrderUtils.ValidValue(unitValue);
            MaterialId = materialId;
            ServiceOrderId = serviceOrderId;
        }

        public ServiceOrderMaterial(ServiceOrderMaterial serviceOrderMaterial, Guid materalID, Guid serviceOrderId)
        {
            Id = serviceOrderMaterial.Id;
            Quantity = ServiceOrderUtils.ValidQuantity(serviceOrderMaterial.Quantity);
            UnitValue = ServiceOrderUtils.ValidValue(serviceOrderMaterial.UnitValue);
            MaterialId = materalID;
            ServiceOrderId = serviceOrderId;
        }
    }
}
