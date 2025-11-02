using FIAPOficina.Application.Tests.Mocks.Repositories;
using FIAPOficina.Application.Tests.Mocks.Services;
using FIAPOficina.Application.Vehicles.Commands.CreateVehicle;
using FIAPOficina.Application.Vehicles.Commands.DeleteVehicle;
using FIAPOficina.Application.Vehicles.Commands.GetAllVehicles;
using FIAPOficina.Application.Vehicles.Commands.GetSingleVehicle;
using FIAPOficina.Application.Vehicles.Commands.UpdateVehicle;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Application.Tests.Vehicles
{
    public class VehiclesTest
    {
        private VehicleRepositoryMock _vehicleRepositoryMock = new VehicleRepositoryMock();
        private ClientsServiceMock _clientServerMock = new ClientsServiceMock();

        [Fact]
        public void Should_Create_Vehicle()
        {
            var service = new VehiclesService(_vehicleRepositoryMock, _clientServerMock);
            var clientId = Guid.NewGuid();

            var vehicle = service.AddAsync(new CreateVehicleCommand("Jeep", "Renegade", 2024, "QHG1J20", "Branco", clientId)).GetAwaiter().GetResult();
            Assert.NotNull(vehicle);
            Assert.Equal("Jeep", vehicle.Brand);
            Assert.Equal("Renegade", vehicle.Model);
            Assert.Equal(2024, vehicle.Year);
            Assert.Equal("QHG1J20", vehicle.Plate);
            Assert.Equal("Branco", vehicle.Color);
            Assert.Equal(clientId, vehicle.ClientId);

            var vehicleDb = service.GetSingleAsync(new GetSingleVehicleCommand(vehicle.Id)).GetAwaiter().GetResult();
            Assert.NotNull(vehicleDb);
            Assert.Equal("Jeep", vehicleDb.Brand);
            Assert.Equal("Renegade", vehicleDb.Model);
            Assert.Equal(2024, vehicleDb.Year);
            Assert.Equal("QHG1J20", vehicleDb.Plate);
            Assert.Equal("Branco", vehicleDb.Color);
            Assert.Equal(clientId, vehicleDb.ClientId);
        }

        [Fact]
        public void Should_Update_Vehicle()
        {
            var services = new VehiclesService(_vehicleRepositoryMock, _clientServerMock);
            var clientId = Guid.NewGuid();
            var allVehicles = services.GetAll(new GetAllVehiclesCommand());
            Vehicle vehicles;
            if (allVehicles is not null && allVehicles.Any())
                vehicles = allVehicles[0];
            else
                vehicles = services.AddAsync(new CreateVehicleCommand("Jeep", "Renegade", 2024, "QHG1J20", "Branco", clientId)).GetAwaiter().GetResult();

            var vehicleUpdate = services.UpdateAsync(new UpdateVehicleCommand(vehicles.Id, "Jeep", "Compass", 2024, "QHG1J20", "Branco")).GetAwaiter().GetResult();

            Assert.NotNull(vehicleUpdate);
            Assert.Equal("Jeep", vehicleUpdate.Brand);
            Assert.Equal("Compass", vehicleUpdate.Model);
            Assert.Equal(2024, vehicleUpdate.Year);
            Assert.Equal("QHG1J20", vehicleUpdate.Plate);
            Assert.Equal("Branco", vehicleUpdate.Color);
            Assert.Equal(clientId, vehicleUpdate.ClientId);
        }

        [Fact]
        public void Should_Delete_Vehicle()
        {
            var services = new VehiclesService(_vehicleRepositoryMock, _clientServerMock); 

            var allVehicles = services.GetAll(new GetAllVehiclesCommand());

            foreach (var vehicles in allVehicles)
                services.DeleteAsync(new DeleteVehicleCommand(vehicles.Id)).GetAwaiter();
            allVehicles = null;
            allVehicles = services.GetAll(new GetAllVehiclesCommand());

            Assert.Empty(allVehicles);
        }
    }
}
