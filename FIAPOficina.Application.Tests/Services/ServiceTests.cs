using FIAPOficina.Application.Services.Commands.CreateService;
using FIAPOficina.Application.Services.Commands.DeleteService;
using FIAPOficina.Application.Services.Commands.GetAllServices;
using FIAPOficina.Application.Services.Commands.GetSingleService;
using FIAPOficina.Application.Services.Commands.UpdateService;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Application.Tests.Mocks.Repositories;
using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Application.Tests.Services
{
    public class ServiceTests
    {
        private ServicesRepositoryMock _mock;

        public ServiceTests()
        {
            _mock = new ServicesRepositoryMock();
        }

        [Fact]
        public void Should_Create_Valid_Service()
        {
            var services = new ServicesService(_mock);

            var service = services.AddAsync(new CreateServiceCommand("Calibrar Pneu", "Calibrar um Pneu", 6.00m)).GetAwaiter().GetResult();

            Assert.NotNull(service);
            Assert.Equal("Calibrar Pneu", service.Name);
            Assert.Equal("Calibrar um Pneu", service.Description);
            Assert.Equal(6.00m, service.Value);

            var serviceDb = services.GetSingleAsync(new GetSingleServiceCommand(service.Id)).GetAwaiter().GetResult();

            Assert.NotNull(serviceDb);
            Assert.Equal("Calibrar Pneu", serviceDb.Name);
            Assert.Equal("Calibrar um Pneu", serviceDb.Description);
            Assert.Equal(6.00m, serviceDb.Value);
        }

        [Fact]
        public void Should_Update_Service()
        {
            var services = new ServicesService(_mock);

            var allServices = services.GetAll(new GetAllServicesCommand());
            Service service;
            if (allServices is not null && allServices.Any())
                service = allServices[0];
            else
                service = services.AddAsync(new CreateServiceCommand("Calibrar Pneu", "Calibrar um Pneu", 6.00m)).GetAwaiter().GetResult();

            var serviceUpdate = services.UpdateAsync(new UpdateServiceCommand(service.Id, "Calibrar Pneu", "Calibrar muito pneu", 6.00m)).GetAwaiter().GetResult();

            Assert.NotNull(serviceUpdate);
            Assert.Equal("Calibrar Pneu", serviceUpdate.Name);
            Assert.Equal("Calibrar muito pneu", serviceUpdate.Description);
            Assert.Equal(6.00m, serviceUpdate.Value);
        }

        [Fact]
        public void Should_Delete_Service()
        {
            var services = new ServicesService(_mock);

            var allServices = services.GetAll(new GetAllServicesCommand());

            foreach (var service in allServices)
                services.DeleteAsync(new DeleteServiceCommand(service.Id)).GetAwaiter();
            allServices = null;
            allServices = services.GetAll(new GetAllServicesCommand());

            Assert.Empty(allServices);
        }
    }
}
