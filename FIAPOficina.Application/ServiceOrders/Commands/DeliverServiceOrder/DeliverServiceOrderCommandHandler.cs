using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.DeliverServiceOrder
{
    internal class DeliverServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public DeliverServiceOrderCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeliverServiceOrderCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.Completed)
            {
                throw new Exception("Only service orders with status \"Completed\" can be set as \"Delivered\"!");
            }

            serviceOrder.Status = ServiceOrderStatus.Delivered;
            _repository.Update(serviceOrder);
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