namespace FIAPOficina.Domain.Services.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }

        public Service(string name, string description, decimal value, Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
            Name = name;
            Description = description;
            Value = value;
        }

        public Service(Service service, Guid id)
        {
            Id = id;
            Name = service.Name;
            Description = service.Description;
            Value = service.Value;
        }
    }
}