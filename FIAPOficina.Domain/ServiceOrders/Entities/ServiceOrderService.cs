using FIAPOficina.Domain.ServiceOrders.Utils;

namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public class ServiceOrderService
    {
        public Guid ServiceId { get; private set; }
        public Guid ServiceOrderId { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal UnitValue { get; private set; }


        public ServiceOrderService(Guid serviceId, Guid serviceOrderId, int quantity, decimal unitValue)
        {
            Quantity = ServiceOrderUtils.ValidQuantity(quantity);
            UnitValue = ServiceOrderUtils.ValidValue(unitValue);
            TotalValue = quantity*unitValue;
            ServiceId = serviceId;
            ServiceOrderId = serviceOrderId;
        }
        public ServiceOrderService(ServiceOrderService serviceOrderService, Guid? serviceId, Guid? serviceOrderId)
        {
            Quantity = ServiceOrderUtils.ValidQuantity(serviceOrderService.Quantity);
            UnitValue = ServiceOrderUtils.ValidValue(serviceOrderService.UnitValue);
            TotalValue = serviceOrderService.TotalValue;

            if (serviceId.HasValue) ServiceId = serviceId.Value;

            if (serviceOrderId.HasValue) ServiceOrderId = serviceOrderId.Value;
        }
    }
}
