using FIAPOficina.Application.ServiceOrders.Commands.ApproveServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.CompleteServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.DeliverServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders;
using FIAPOficina.Application.ServiceOrders.Commands.GetSingleServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.RejectServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.RequestServiceOrderApproval;
using FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrderDiagnosis;
using FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder;
using FIAPOficina.Application.Tests.Mocks.Repositories;
using FIAPOficina.Application.Tests.Mocks.Services;
using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Application.Tests.ServiceOrders
{
    public class ServiceOrderTests
    {
        private ServiceOrderRepositoryMock _repository = new ServiceOrderRepositoryMock();
        private ServicesServiceMock _servicesService = new ServicesServiceMock();
        private ClientsServiceMock _clientsService = new ClientsServiceMock();
        private MaterialsServiceMock _materialsService = new MaterialsServiceMock();
        private VehiclesServiceMock _vehicleService = new VehiclesServiceMock();
        private MailsServiceMock _mailService = new MailsServiceMock();

        [Fact]
        public void Should_Create_ServiceOrder()
        {
            var serviceOrderService = CreateServiceOrderService();
            var vehicleId = Guid.NewGuid();
            var osService =new List<ServiceOrderServiceToCreate>();
            osService.Add(new ServiceOrderServiceToCreate(Guid.Parse("B66A78BF-A800-4F18-B052-CEADF34558A7"), 3));
            var osMaterial = new List<ServiceOrderMaterialToCreate>();
            osMaterial.Add(new ServiceOrderMaterialToCreate(Guid.Parse("CE91D1FC-DBF1-4AB1-9D10-F69C25E10C5B"), 3));
            var serviceOrder = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), osService, osMaterial)).GetAwaiter().GetResult();

            Assert.NotNull(serviceOrder);
            Assert.NotNull(serviceOrder.Materials);
            Assert.NotEmpty(serviceOrder.Materials);
            Assert.NotNull(serviceOrder.Services);
            Assert.NotEmpty(serviceOrder.Services);
            Assert.Equal(ServiceOrderStatus.Received, serviceOrder.Status);

            var os = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(serviceOrder.Id)).GetAwaiter().GetResult();
            Assert.NotNull(os);
            Assert.NotNull(os.Materials);
            Assert.NotEmpty(os.Materials);
            Assert.NotNull(os.Services);
            Assert.NotEmpty(os.Services);
            Assert.Equal(ServiceOrderStatus.Received, os.Status);
        }

        [Fact]
        public void Should_Update_ServiceOrder()
        {
            var serviceOrderService = CreateServiceOrderService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            var osService = new List<ServiceOrderServiceToCreate>();
            osService.Add(new ServiceOrderServiceToCreate(Guid.Parse("B66A78BF-A800-4F18-B052-CEADF34558A7"), 3));
            var osMaterial = new List<ServiceOrderMaterialToCreate>();
            osMaterial.Add(new ServiceOrderMaterialToCreate(Guid.Parse("CE91D1FC-DBF1-4AB1-9D10-F69C25E10C5B"), 3));
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), osService, osMaterial)).GetAwaiter().GetResult();

            var osServiceUP = new List<ServiceOrderServiceToUpdate>();
            osServiceUP.Add(new ServiceOrderServiceToUpdate(os.Services.FirstOrDefault().Id, Guid.Parse("B66A78BF-A800-4F18-B052-CEADF34558A7"), 3));
            var osMaterialUP = new List<ServiceOrderMaterialToUpdate>();
            osMaterialUP.Add(new ServiceOrderMaterialToUpdate(os.Materials.FirstOrDefault().Id,Guid.Parse("CE91D1FC-DBF1-4AB1-9D10-F69C25E10C5B"), 3));

            var osUpdate = service.UpdateAsync(new UpdateServiceOrderCommand(os.Id, os.VehicleId, osServiceUP, osMaterialUP)).GetAwaiter().GetResult();

            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Received, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_StartDiagnosis()
        {
            var serviceOrderService = CreateServiceOrderService();

            var allOS = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = serviceOrderService.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            serviceOrderService.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.InDiagnosis, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_WatingApproval()
        {
            var serviceOrderService = CreateServiceOrderService();
            var material = CreateMaterial();
            var service = CreateService();

            Guid osId = Guid.NewGuid();
            var allOS = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;

            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = serviceOrderService.AddAsync(new CreateServiceOrderCommand(osId, new List<ServiceOrderServiceToCreate>() { new ServiceOrderServiceToCreate(service.Id, 1) }, new List<ServiceOrderMaterialToCreate>() { new ServiceOrderMaterialToCreate(material.Id, 1) })).GetAwaiter().GetResult();
            serviceOrderService.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.WaitingApproval, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Aproved()
        {
            var serviceOrderService = CreateServiceOrderService();
            var material = CreateMaterial();
            var service = CreateService();

            var allOS = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = serviceOrderService.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>() { new ServiceOrderServiceToCreate(service.Id, 1) }, new List<ServiceOrderMaterialToCreate>() { new ServiceOrderMaterialToCreate(material.Id, 1) })).GetAwaiter().GetResult();

            serviceOrderService.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();

            var osUpdate = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Approved, osUpdate.Status);
        }

        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Rejected()
        {
            var serviceOrderService = CreateServiceOrderService();
            var material = CreateMaterial();
            var service = CreateService();

            var allOS = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = serviceOrderService.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>() { new ServiceOrderServiceToCreate(service.Id, 1) }, new List<ServiceOrderMaterialToCreate>() { new ServiceOrderMaterialToCreate(material.Id, 1) })).GetAwaiter().GetResult();

            serviceOrderService.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.RejectServiceOrder(new RejectServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();

            var osUpdate = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Rejected, osUpdate.Status);
        }

        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Running()
        {
            var serviceOrderService = CreateServiceOrderService();
            var material = CreateMaterial();
            var service = CreateService();

            var allOS = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = serviceOrderService.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>() { new ServiceOrderServiceToCreate(service.Id, 1) }, new List<ServiceOrderMaterialToCreate>() { new ServiceOrderMaterialToCreate(material.Id, 1) })).GetAwaiter().GetResult();

            serviceOrderService.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();
            serviceOrderService.StartServiceOrder(new StartServiceOrderCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Running, osUpdate.Status);
        }

        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Completed()
        {
            var serviceOrderService = CreateServiceOrderService();
            var material = CreateMaterial();
            var service = CreateService();

            var allOS = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = serviceOrderService.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>() { new ServiceOrderServiceToCreate(service.Id, 1) }, new List<ServiceOrderMaterialToCreate>() { new ServiceOrderMaterialToCreate(material.Id, 1) })).GetAwaiter().GetResult();

            serviceOrderService.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();
            serviceOrderService.StartServiceOrder(new StartServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.CompleteServiceOrder(new CompleteServiceOrderCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Completed, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Delivered()
        {
            var serviceOrderService = CreateServiceOrderService();
            var material = CreateMaterial();
            var service = CreateService();

            var allOS = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = serviceOrderService.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>() { new ServiceOrderServiceToCreate(service.Id, 1) }, new List<ServiceOrderMaterialToCreate>() { new ServiceOrderMaterialToCreate(material.Id, 1) })).GetAwaiter().GetResult();

            serviceOrderService.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();
            serviceOrderService.StartServiceOrder(new StartServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.CompleteServiceOrder(new CompleteServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            serviceOrderService.DeliverServiceOrder(new DeliverServiceOrderCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = serviceOrderService.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Delivered, osUpdate.Status);
        }


        [Fact]
        public void Should_Delete_All_ServiceOrders()
        {
            var service = CreateService();
            var os1 = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();
            var allServiceOrders = service.GetAll(new GetAllServiceOrdersCommand());

            foreach (var os in allServiceOrders)
                serviceOrderService.DeleteAsync(new Application.ServiceOrders.Commands.DeleteServiceOrder.DeleteServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            allServiceOrders = null;
            allServiceOrders = serviceOrderService.GetAll(new GetAllServiceOrdersCommand());
            Assert.Empty(allServiceOrders);
        }

        private Application.ServiceOrders.Services.ServiceOrderService CreateServiceOrderService()
        {
            return new Application.ServiceOrders.Services.ServiceOrderService(_repository, _vehicleService, _materialsService, _clientsService, _servicesService, _mailService);
        }

        private Material CreateMaterial()
        {
            return _materialsService.AddAsync(new("Name", "Description", "Brand", 10, 10)).GetAwaiter().GetResult();
        }

        private Service CreateService()
        {
            return _servicesService.AddAsync(new("Name", "Description", 10)).GetAwaiter().GetResult();
        }
    }
}
