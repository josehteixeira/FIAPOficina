using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.RequestServiceOrderApproval
{
    internal class RequestServiceOrderApprovalCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public RequestServiceOrderApprovalCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RequestServiceOrderApprovalCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.InDiagnosis)
            {
                throw new Exception("Only service orders with status \"In diagnosis\" can request approval!");
            }

            serviceOrder.Status = ServiceOrderStatus.Delivered;
            await _repository.UpdateAsync(serviceOrder).ConfigureAwait(false);

            SendMail(serviceOrder);
        }

        private void SendMail(ServiceOrder serviceOrder)
        {
            string mailHtmlBody = CreateMailHtmlBody(serviceOrder);
            //send email
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