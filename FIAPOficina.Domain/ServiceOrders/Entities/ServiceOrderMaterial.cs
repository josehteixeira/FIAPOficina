using FIAPOficina.Domain.Materials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.ServiceOrders.Entities
{
    public class ServiceOrderMaterial
    {
        public Guid MaterialId { get; private set; }
        public Guid ServiceOrderId {  get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalValue{ get; private set; }
        public decimal UnitValue{ get; private set; }
        
        public ServiceOrderMaterial(Guid materialId, Guid serviceOrderId, int quanity, decimal unitValue)
        {
            MaterialId = materialId;
            ServiceOrderId = serviceOrderId;
            Quantity = quanity;
            UnitValue = unitValue;
            TotalValue = unitValue * quanity;
        }
        public ServiceOrderMaterial(ServiceOrderMaterial serviceOrderMaterial, Guid? materalID,Guid? serviceOrderId)
        {
            Quantity = serviceOrderMaterial.Quantity;
            UnitValue = serviceOrderMaterial.UnitValue;
            TotalValue = serviceOrderMaterial.TotalValue;

            if (materalID.HasValue) MaterialId = materalID.Value;

            if (serviceOrderId.HasValue) ServiceOrderId = serviceOrderId.Value;
        }

    }
}
