using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.DeleteServiceOrder
{
    internal class DeleteServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public DeleteServiceOrderCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteServiceOrderCommand command)
        {
            await _repository.DeleteAsync(command.Id).ConfigureAwait(false);
        }
    }
}