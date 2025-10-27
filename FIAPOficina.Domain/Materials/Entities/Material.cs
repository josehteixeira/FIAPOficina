using System.Xml.Linq;

namespace FIAPOficina.Domain.Materials.Entities
{
    public class Material
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }

        public Material(string name, string description, string brand, decimal value, Guid? id = null, int quantity = 0)
        {
            Name = name;
            Description = description;
            Brand = brand;
            Value = value;
            Quantity = quantity;

            if (id.HasValue) Id = id.Value;
        }

        public Material(Material material, Guid? id)
        {
            Name = material.Name;
            Description = material.Description;
            Brand = material.Brand;
            Value = material.Value;
            Quantity = material.Quantity;
            if (id.HasValue) Id = id.Value;
        }
    }
}
