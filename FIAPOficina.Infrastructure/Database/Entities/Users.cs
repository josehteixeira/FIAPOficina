namespace FIAPOficina.Infrastructure.Database.Entities
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}