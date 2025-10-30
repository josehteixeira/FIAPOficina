namespace FIAPOficina.Domain.Clients.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// CPF or CNPJ
        /// </summary>
        public string Identifier { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Client(string name, string identifier, string phone, string email, string address, Guid? id = null)
        {
            if (!Utils.ClientUtils.IsValidDocument(identifier))
            {
                throw new ArgumentException("Identifier not recognized as a valid CPF or CPNJ", nameof(Identifier));
            }

            if (!Utils.ClientUtils.IsValidMail(email))
            {
                throw new ArgumentException("Invalid email", nameof(Email));
            }

            Id = id.HasValue ? id.Value : Guid.NewGuid();
            Name = name;
            Identifier = identifier;
            Phone = phone;
            Email = email;
            Address = address;
        }

        public Client(Client client, Guid id)
        {
            if (!Utils.ClientUtils.IsValidDocument(client.Identifier))
            {
                throw new ArgumentException("Identifier not recognized as a valid CPF or CPNJ", nameof(Identifier));
            }

            if (!Utils.ClientUtils.IsValidMail(client.Email))
            {
                throw new ArgumentException("Invalid email", nameof(Email));
            }

            Id = id;
            Name = client.Name;
            Identifier = client.Identifier;
            Phone = client.Phone;
            Email = client.Email;
            Address = client.Address;
        }
    }
}