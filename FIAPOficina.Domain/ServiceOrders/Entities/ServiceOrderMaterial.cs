using FIAPOficina.Domain.ServiceOrders.Utils;

namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public class ServiceOrderMaterial
    {
        public Guid MaterialId { get; private set; }
        public Guid ServiceOrderId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal UnitValue { get; private set; }

        public ServiceOrderMaterial(Guid materialId, Guid serviceOrderId, int quantity, decimal unitValue)
        {
            Quantity = ServiceOrderUtils.ValidQuantity(quantity);
            UnitValue = ServiceOrderUtils.ValidValue(unitValue);
            MaterialId = materialId;
            ServiceOrderId = serviceOrderId;
            TotalValue = Quantity * UnitValue;
        }
        public ServiceOrderMaterial(ServiceOrderMaterial serviceOrderMaterial, Guid? materalID, Guid? serviceOrderId)
        {
            Quantity = ServiceOrderUtils.ValidQuantity(serviceOrderMaterial.Quantity);
            UnitValue = ServiceOrderUtils.ValidValue(serviceOrderMaterial.UnitValue);
            TotalValue = serviceOrderMaterial.TotalValue;

            if (materalID.HasValue) MaterialId = materalID.Value;

            if (serviceOrderId.HasValue) ServiceOrderId = serviceOrderId.Value;
        }

    }
}
