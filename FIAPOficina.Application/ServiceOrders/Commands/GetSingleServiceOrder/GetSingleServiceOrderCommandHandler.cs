using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.GetSingleServiceOrder
{
    internal class GetSingleServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public GetSingleServiceOrderCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceOrder?> Handle(GetSingleServiceOrderCommand command)
        {
            return await _repository.FirstOrDefaultAsync(command.Id).ConfigureAwait(false);
        }
    }
}