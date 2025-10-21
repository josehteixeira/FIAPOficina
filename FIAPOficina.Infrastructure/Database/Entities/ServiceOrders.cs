using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class ServiceOrders
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicles Vehicle { get; set; }
        public ICollection<ServiceOrderServices> Services { get; set; }
        public ICollection<ServiceOrderMaterials> Materials { get; set; }
        public int Status { get; set; }
    }
}
