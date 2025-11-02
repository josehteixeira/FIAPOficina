using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Domain.Tests.Vehicles
{
    public class VehicleCreationTests
    {
        [Fact]
        public void Should_Create_Valid_Vehicle_With_New_Plate_Format()
        {
            var clienteId = Guid.NewGuid();

            Vehicle service = new Vehicle("Jeep", "Renegade", 2024, "QHG1J20", "Branco", clienteId);
            Assert.NotNull(service);
            Assert.Equal("Jeep", service.Brand);
            Assert.Equal("Renegade", service.Model);
            Assert.Equal(2024, service.Year);
            Assert.Equal("QHG1J20", service.Plate);
            Assert.Equal("Branco", service.Color);
            Assert.Equal(clienteId, service.ClientId);
        }

        [Fact]
        public void Should_Create_Valid_Vehicle_With_OLD_Plate_Format()
        {
            var clienteId = Guid.NewGuid();

            Vehicle service = new Vehicle("Jeep", "Renegade", 2024, "QHG1320", "Branco", clienteId);
            Assert.NotNull(service);
            Assert.Equal("Jeep", service.Brand);
            Assert.Equal("Renegade", service.Model);
            Assert.Equal(2024, service.Year);
            Assert.Equal("QHG1320", service.Plate);
            Assert.Equal("Branco", service.Color);
            Assert.Equal(clienteId, service.ClientId);
        }

        [Fact]
        public void Should_Throw_ArgumentException_By_Plate()
        {
            var clienteId = Guid.NewGuid();
            Assert.Throws<ArgumentException>(() => new Vehicle("Jeep", "Renegade", 2024, "QH11J20", "Branco", clienteId));
        }
    }
}
