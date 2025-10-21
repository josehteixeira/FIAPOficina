using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.Materials.Entities
{
    public class Material
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Value { get; set; }

        public Material(string name, string description, string brand, decimal value, Guid? id = null)
        {
            Name = name;
            Description = description;
            Brand = brand;
            Value = value;

            if (id.HasValue) Id = id.Value;
        }
    }
}
