using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Domain.Tests.Services
{
    public class ServiceCreationTests
    {
        [Fact]
        public void Should_Create_Valid_Service()
        {
            Service service = new Service("Calibrar Pneu", "Calibrar um Pneu", 6.00m);
            Assert.NotNull(service);
            Assert.Equal("Calibrar Pneu", service.Name);
            Assert.Equal("Calibrar um Pneu", service.Description);
            Assert.Equal(6.00m, service.Value);
        }
        [Fact]
        public void Should_Create_Valid_Service_By_Existent_Service()
        {
            Service service = new Service("Calibrar Pneu", "Calibrar um Pneu", 6.00m);

            var newService = new Service(service, Guid.NewGuid());
            Assert.NotNull(newService);
            Assert.Equal(service.Name, newService.Name);
            Assert.Equal(service.Description, newService.Description);
            Assert.Equal(service.Value, newService.Value);
        }

        [Fact]
        public void Should_Throw_ArgumentException_By_Value()
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=> new Service("Calibrar Pneu", "Calibrar um Pneu", -6.00m));
        }
    }
}
