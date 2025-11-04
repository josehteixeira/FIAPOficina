using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrderDiagnosis
{
    internal class StartServiceOrderDiagnosisCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public StartServiceOrderDiagnosisCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(StartServiceOrderDiagnosisCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.Received)
            {
                throw new Exception("Only service orders with status \"Received\" can be set as \"In diagnosis\"!");
            }

            serviceOrder.Status = ServiceOrderStatus.InDiagnosis;
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