using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class Materials
    {
         public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Value { get; set; }
        public ICollection<ServiceOrderMaterials> ServiceOrderMaterials { get; set; }

    }
}
