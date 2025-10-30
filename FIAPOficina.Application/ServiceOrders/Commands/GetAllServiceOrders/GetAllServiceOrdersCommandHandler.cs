using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders
{
    internal class GetAllServiceOrdersCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public GetAllServiceOrdersCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public ServiceOrder[] Handle(GetAllServiceOrdersCommand command)
        {
            return _repository.GetAll(command.VehicleId, command.ClientId);
        }
    }
}