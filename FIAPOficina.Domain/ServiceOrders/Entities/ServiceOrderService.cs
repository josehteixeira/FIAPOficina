using FIAPOficina.Domain.Utils;

namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public class ServiceOrderService
    {
        public Guid Id { get; private set; }
        public Guid ServiceId { get; private set; }
        public Guid ServiceOrderId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalValue { get => UnitValue * Quantity; }
        public decimal UnitValue { get; private set; }


        public ServiceOrderService(Guid serviceId, Guid serviceOrderId, int quantity, decimal unitValue, Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
            Quantity = UtilsCommon.ValidQuantity(quantity);
            UnitValue = UtilsCommon.ValidValue(unitValue);
            ServiceId = serviceId;
            ServiceOrderId = serviceOrderId;
        }

        public ServiceOrderService(ServiceOrderService serviceOrderService, Guid serviceId, Guid serviceOrderId)
        {
            Id = serviceOrderService.Id;
            Quantity = UtilsCommon.ValidQuantity(serviceOrderService.Quantity);
            UnitValue = UtilsCommon.ValidValue(serviceOrderService.UnitValue);
            ServiceId = serviceId;
            ServiceOrderId = serviceOrderId;
        }
    }
}
