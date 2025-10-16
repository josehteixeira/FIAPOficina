using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;

namespace FIAPOficina.Application.Vehicles.Commands.GetAllVehicles
{
    internal class GetAllVehiclesCommandHandler
    {
        private readonly IVehicleRepository _repository;

        public GetAllVehiclesCommandHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public Vehicle[] Handle(GetAllVehiclesCommand command)
        {
            return _repository.GetAll(command.ClientId);
        }
    }
}