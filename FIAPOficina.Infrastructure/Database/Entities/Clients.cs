namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class Clients
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Identifier { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ICollection<Vehicles> Vehicles { get; set; } = null!;
    }
}