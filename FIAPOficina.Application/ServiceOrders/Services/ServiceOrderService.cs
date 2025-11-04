using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Application.Common.Mail;
using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Application.ServiceOrders.Commands.ApproveServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.CompleteServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.DeleteServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.DeliverServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders;
using FIAPOficina.Application.ServiceOrders.Commands.GetAverageTime;
using FIAPOficina.Application.ServiceOrders.Commands.GetServicesOrderByVehicle;
using FIAPOficina.Application.ServiceOrders.Commands.GetSingleServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.RejectServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.RequestServiceOrderApproval;
using FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrderDiagnosis;
using FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Services
{
    public class ServiceOrderService : IServiceOrderService
    {
        private readonly CreateServiceOrderCommandHandler _createHandler;
        private readonly UpdateServiceOrderCommandHandler _updateHandler;
        private readonly DeleteServiceOrderCommandHandler _deleteHandler;
        private readonly GetSingleServiceOrderCommandHandler _querySingleHandler;
        private readonly GetAllServiceOrdersCommandHandler _queryAllHandler;
        private readonly ApproveServiceOrderCommandHandler _approveHandler;
        private readonly CompleteServiceOrderCommandHandler _completeHandler;
        private readonly DeliverServiceOrderCommandHandler _deliverHandler;
        private readonly RejectServiceOrderCommandHandler _rejectHandler;
        private readonly RequestServiceOrderApprovalCommandHandler _requestApprovalHandler;
        private readonly StartServiceOrderCommandHandler _startHandler;
        private readonly StartServiceOrderDiagnosisCommandHandler _startDiagnosisHandler;
        private readonly GetServicesOrderByVehicleCommandHandler _getServicesByVehicleHandler;
        private readonly GetAverageTimeCommandHandler _getAverageTimeHandler;

        public ServiceOrderService(
            IServiceOrderRepository repository,
            IVehiclesService vehiclesService,
            IMaterialsService materialsService,
            IClientsService clientsService,
            IServicesService servicesService,
            IMailService mailService)
        {
            _createHandler = new(repository, vehiclesService, materialsService, servicesService);
            _updateHandler = new(repository, materialsService, servicesService);
            _deleteHandler = new(repository);
            _querySingleHandler = new(repository);
            _queryAllHandler = new(repository);
            _approveHandler = new(repository, vehiclesService, clientsService);
            _completeHandler = new(repository);
            _deliverHandler = new(repository);
            _rejectHandler = new(repository, vehiclesService, clientsService);
            _requestApprovalHandler = new(repository, mailService, vehiclesService, clientsService, servicesService, materialsService);
            _startHandler = new(repository, materialsService);
            _startDiagnosisHandler = new(repository);
            _getServicesByVehicleHandler = new(repository, vehiclesService, clientsService);
            _getAverageTimeHandler = new(repository);
        }

        public async Task<ServiceOrder> AddAsync(CreateServiceOrderCommand command)
        {
            return await _createHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task ApproveServiceOrder(ApproveServiceOrderCommand command)
        {
            await _approveHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task CompleteServiceOrder(CompleteServiceOrderCommand command)
        {
            await _completeHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task DeleteAsync(DeleteServiceOrderCommand command)
        {
            await _deleteHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task DeliverServiceOrder(DeliverServiceOrderCommand command)
        {
            await _deliverHandler.Handle(command).ConfigureAwait(false);
        }

        public ServiceOrder[] GetAll(GetAllServiceOrdersCommand command)
        {
            return _queryAllHandler.Handle(command);
        }

        public TimeSpan? GetAverageTime(GetAverageTimeCommand command)
        {
            return _getAverageTimeHandler.Handle(command);
        }

        public async Task<ServiceOrder[]> GetServicesOrderByVehicle(GetServicesOrderByVehicleCommand command)
        {
            return await _getServicesByVehicleHandler.Handle(command);
        }

        public async Task<ServiceOrder?> GetSingleAsync(GetSingleServiceOrderCommand command)
        {
            return await _querySingleHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task RejectServiceOrder(RejectServiceOrderCommand command)
        {
            await _rejectHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task RequestServiceOrderApproval(RequestServiceOrderApprovalCommand command)
        {
            await _requestApprovalHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task StartServiceOrder(StartServiceOrderCommand command)
        {
            await _startHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task StartServiceOrderDiagnosis(StartServiceOrderDiagnosisCommand command)
        {
            await _startDiagnosisHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task<ServiceOrder> UpdateAsync(UpdateServiceOrderCommand command)
        {
            return await _updateHandler.Handle(command).ConfigureAwait(false);
        }
    }
}