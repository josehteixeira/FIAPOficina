using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.CompleteServiceOrder
{
    internal class CompleteServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public CompleteServiceOrderCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CompleteServiceOrderCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.Running)
            {
                throw new Exception("Only service orders with status \"Running\" can be set as \"Complete\"!");
            }

            serviceOrder.Status = ServiceOrderStatus.Completed;
            serviceOrder.FinishedOn = DateTime.Now;
            await _repository.UpdateAsync(serviceOrder).ConfigureAwait(false);
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