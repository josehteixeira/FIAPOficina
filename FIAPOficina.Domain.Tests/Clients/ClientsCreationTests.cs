using FIAPOficina.Domain.Clients.Entities;

namespace FIAPOficina.Domain.Tests.Clients
{
    public class ClientsCreationTests
    {
        [Fact]
        public void Should_Create_Valid_Clients()
        {

            Client client = new Client("Emanuel Fontes", "96202913010", "47 999828521", "teste@teste.com", "Rua das couves");
            Assert.NotNull(client);
            Assert.Equal("Emanuel Fontes", client.Name);
            Assert.Equal("96202913010", client.Identifier);
            Assert.Equal("47 999828521", client.Phone);
            Assert.Equal("teste@teste.com", client.Email);
            Assert.Equal("Rua das couves", client.Address);
        }

        [Fact]
        public void Should_Throw_ArgumentException_By_Identifier()
        {
            Assert.Throws<ArgumentException>(() => new Client("Emanuel Fontes", "9620291AA10", "47 999828521", "teste@teste.com", "Rua das couves"));
        }

        [Fact]
        public void Should_Throw_ArgumentException_By_Email()
        {
            Assert.Throws<ArgumentException>(() => new Client("Emanuel Fontes", "96202913010", "47 999828521", "erro.com", "Rua das couves"));
        }
    }
}
