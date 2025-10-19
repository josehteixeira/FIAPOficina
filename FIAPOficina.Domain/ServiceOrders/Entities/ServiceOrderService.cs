using FIAPOficina.Domain.Materials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ServiceId = serviceId;
            ServiceOrderId = serviceOrderId;
            Quantity = quantity;
            UnitValue = unitValue;
            TotalValue = quantity*unitValue;
        }
        public ServiceOrderService(ServiceOrderService serviceOrderService, Guid? serviceId, Guid? serviceOrderId)
        {
            Quantity = serviceOrderService.Quantity;
            UnitValue = serviceOrderService.UnitValue;
            TotalValue= serviceOrderService.TotalValue;

            if (serviceId.HasValue) ServiceId = serviceId.Value;

            if (serviceOrderId.HasValue) ServiceOrderId = serviceOrderId.Value;
        }
    }
}
