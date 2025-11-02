using FIAPOficina.Application.Clients.Commands.GetSingleClient;
using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Application.Common.Mail;
using FIAPOficina.Application.Vehicles.Commands.GetSingleVehicle;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.RequestServiceOrderApproval
{
    internal class RequestServiceOrderApprovalCommandHandler
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IMailService _mailService;
        private readonly IVehiclesService _vehiclesService;
        private readonly IClientsService _clientsService;

        public RequestServiceOrderApprovalCommandHandler(IServiceOrderRepository repository, IMailService mailService, IVehiclesService vehiclesService, IClientsService clientsService)
        {
            _repository = repository;
            _mailService = mailService;
            _vehiclesService = vehiclesService;
            _clientsService = clientsService;
        }

        public async Task Handle(RequestServiceOrderApprovalCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.InDiagnosis)
            {
                throw new Exception("Only service orders with status \"In diagnosis\" can request approval!");
            }

            serviceOrder.Status = ServiceOrderStatus.WaitingApproval;
            await _repository.UpdateAsync(serviceOrder).ConfigureAwait(false);

            SendMail(serviceOrder);
        }

        private void SendMail(ServiceOrder serviceOrder)
        {
            string mailHtmlBody = CreateMailHtmlBody(serviceOrder);
            var vehicle = _vehiclesService.GetSingleAsync(new GetSingleVehicleCommand(serviceOrder.VehicleId)).GetAwaiter().GetResult();
            var client = _clientsService.GetSingleAsync(new GetSingleClientCommand(vehicle.ClientId)).GetAwaiter().GetResult();
            _mailService.SendMail("", "Orçamento do seu veiculo", client?.Email, mailHtmlBody);
        }

        private string CreateMailHtmlBody(ServiceOrder serviceOrder)
        {
            //TODO
            return "";
        }

        private async Task<ServiceOrder> GetServiceOrder(Guid id)
        {
            var oldServiceOrder = await _repository.FirstOrDefaultAsync(id).ConfigureAwait(false);

            if (oldServiceOrder is null)
            {
                throw new Exception("Service order not found!");
            }

            return oldServiceOrder;
        }
    }
}