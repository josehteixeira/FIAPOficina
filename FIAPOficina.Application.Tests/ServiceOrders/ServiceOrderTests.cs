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
using FIAPOficina.Domain.ServiceOrders.Entities;

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

            var osUpdate = service.UpdateAsync(new UpdateServiceOrderCommand(os.Id, os.VehicleId, new List<ServiceOrderServiceToUpdate>(), new List<ServiceOrderMaterialToUpdate>())).GetAwaiter().GetResult();

            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Received, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_StartDiagnosis()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            service.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = service.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.InDiagnosis, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_WatingApproval()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();
            service.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            service.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = service.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.WaitingApproval, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Aproved()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            service.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            service.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            service.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();

            var osUpdate = service.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Approved, osUpdate.Status);
        }

        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Rejected()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            service.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            service.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            service.RejectServiceOrder(new RejectServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();

            var osUpdate = service.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Rejected, osUpdate.Status);
        }

        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Running()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            service.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            service.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            service.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();
            service.StartServiceOrder(new StartServiceOrderCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = service.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Running, osUpdate.Status);
        }

        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Completed()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            service.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            service.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            service.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();
            service.StartServiceOrder(new StartServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            service.CompleteServiceOrder(new CompleteServiceOrderCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = service.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Completed, osUpdate.Status);
        }
        [Fact]
        public void Should_Change_ServiceOrderStatus_To_Delivered()
        {
            var service = CreateService();

            var allOS = service.GetAll(new GetAllServiceOrdersCommand());
            ServiceOrder os;
            if (allOS is not null && allOS.Any())
                os = allOS[0];
            else
                os = service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            service.StartServiceOrderDiagnosis(new StartServiceOrderDiagnosisCommand(os.Id)).GetAwaiter().GetResult();
            service.RequestServiceOrderApproval(new RequestServiceOrderApprovalCommand(os.Id)).GetAwaiter().GetResult();
            service.ApproveServiceOrder(new ApproveServiceOrderCommand(os.Id, "96202913010", "QHH8H99")).GetAwaiter().GetResult();
            service.StartServiceOrder(new StartServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            service.CompleteServiceOrder(new CompleteServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            service.DeliverServiceOrder(new DeliverServiceOrderCommand(os.Id)).GetAwaiter().GetResult();

            var osUpdate = service.GetSingleAsync(new GetSingleServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            Assert.NotNull(osUpdate);
            Assert.Equal(ServiceOrderStatus.Delivered, osUpdate.Status);
        }


        [Fact]
        public void Should_Delete_All_ServiceOrders()
        {
            var service = CreateService();

            var allServiceOrders = service.GetAll(new GetAllServiceOrdersCommand());

            if (allServiceOrders is null || allServiceOrders.Any())
                service.AddAsync(new CreateServiceOrderCommand(Guid.NewGuid(), new List<ServiceOrderServiceToCreate>(), new List<ServiceOrderMaterialToCreate>())).GetAwaiter().GetResult();

            foreach (var os in allServiceOrders)
                service.DeleteAsync(new Application.ServiceOrders.Commands.DeleteServiceOrder.DeleteServiceOrderCommand(os.Id)).GetAwaiter().GetResult();
            allServiceOrders = null;
            allServiceOrders = service.GetAll(new GetAllServiceOrdersCommand());
            Assert.Empty(allServiceOrders);
        }

        private Application.ServiceOrders.Services.ServiceOrderService CreateService()
        {
            return new Application.ServiceOrders.Services.ServiceOrderService(_repository, _vehicleService, _materialsService, _clientsService, _servicesService, _mailService);
        }
    }
}
