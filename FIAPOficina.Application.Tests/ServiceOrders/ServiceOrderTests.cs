using FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders;
using FIAPOficina.Application.ServiceOrders.Commands.GetSingleServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Services;
using FIAPOficina.Application.Tests.Mocks.Repositories;
using FIAPOficina.Application.Tests.Mocks.Services;
using FIAPOficina.Domain.ServiceOrders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.ServiceOrders
{
    public class ServiceOrderTests
    {
        private ServiceOrderRepositoryMock _repository = new ServiceOrderRepositoryMock();
        private ServicesServiceMock _servicesService = new ServicesServiceMock();
        private MaterialsServiceMock _materialsService = new MaterialsServiceMock();
        private VehicleServiceMock _vehicleService = new VehicleServiceMock();

        [Fact]
        public void Should_Create_ServiceOrder()
        {
            var service = CreateService();
            var vehicleId = Guid.NewGuid();
            var serviceOrder = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            Assert.NotNull(serviceOrder);
            Assert.NotNull(serviceOrder.Materials);
            Assert.NotNull(serviceOrder.Services);
            Assert.Equal(ServiceOrderStatus.Received, serviceOrder.Status);

            var os = service.GetSingleAsync(new GetSingleServiceOrderCommand(serviceOrder.Id)).GetAwaiter().GetResult();
            Assert.NotNull(os);
            Assert.NotNull(os.Materials);
            Assert.NotNull(os.Services);
            Assert.Equal(ServiceOrderStatus.Received, os.Status);
        }

        [Fact]
        public void Should_Update_ServiceOrder()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            var osUpdate = service.UpdateAsync(new UpdateServiceOrderCommand(os.Id, os.VehicleId,ServiceOrderStatus.Running, new List<ServiceOrderServiceToUpdate>(), new List<ServiceOrderMaterialToUpdate>())).GetAwaiter().GetResult();

            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Running, osUpdate.Status);
        }
        [Fact]
        public void Should_Delete_All_ServiceOrders()
        {
            var service = CreateService();

            var allServiceOrders = service.GetAll(new GetAllServiceOrdersCommand());

            foreach (var os in allServiceOrders)
                service.DeleteAsync(new Application.ServiceOrders.Commands.DeleteServiceOrder.DeleteServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            allServiceOrders = null;
            allServiceOrders = service.GetAll(new GetAllServiceOrdersCommand());
            Assert.Empty(allServiceOrders);
        }
        private Application.ServiceOrders.Services.ServiceOrderService CreateService()
        {
            return new Application.ServiceOrders.Services.ServiceOrderService(_repository, _vehicleService, _materialsService, _servicesService);
        }
    }
}
