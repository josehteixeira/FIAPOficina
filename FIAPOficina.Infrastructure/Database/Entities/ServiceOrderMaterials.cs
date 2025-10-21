using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class ServiceOrderMaterials
    {
        public Guid MaterialId { get; set; }
        public Materials Materials { get; set; }
        public Guid ServiceOrderId { get; set; }
        public ServiceOrders ServiceOrder { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public decimal UnitValue { get; set; }
    }
}
