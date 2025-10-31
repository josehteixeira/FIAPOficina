using FIAPOficina.Domain.Utils;

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

        public Material(string name, string description, string brand, decimal value, int quantity = 0, Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
            Name = name;
            Description = description;
            Brand = brand;
            Quantity = UtilsCommon.ValidQuantity(quantity);
            Value = UtilsCommon.ValidValue(value);
        }

        public Material(Material material, Guid id)
        {
            Id = id;
            Name = material.Name;
            Description = material.Description;
            Brand = material.Brand;
            Quantity = UtilsCommon.ValidQuantity(material.Quantity);
            Value = UtilsCommon.ValidValue(material.Value);
        }
    }
}