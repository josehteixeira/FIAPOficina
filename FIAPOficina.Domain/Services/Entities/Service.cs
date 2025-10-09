namespace FIAPOficina.Domain.Services.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }

        public Service(string name, string description, double value, Guid? id = null)
        {
            Name = name;
            Description = description;
            Value = value;

            if (id.HasValue) Id = id.Value;
        }

        public Service(Service service, Guid? id = null)
        {
            Name = service.Name;
            Description = service.Description;
            Value = service.Value;

            if (id.HasValue) Id = id.Value;
        }
    }
}