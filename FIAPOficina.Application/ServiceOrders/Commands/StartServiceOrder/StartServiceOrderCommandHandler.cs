using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrder
{
    internal class StartServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public StartServiceOrderCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(StartServiceOrderCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.Approved)
            {
                throw new Exception("Only service orders with status \"Approved\" can be set as \"Running\"!");
            }

            serviceOrder.Status = ServiceOrderStatus.Running;
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