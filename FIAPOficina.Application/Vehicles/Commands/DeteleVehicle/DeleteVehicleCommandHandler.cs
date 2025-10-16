using FIAPOficina.Domain.Vehicles.Repositories;

namespace FIAPOficina.Application.Vehicles.Commands.DeleteVehicle
{
    internal class DeleteVehicleCommandHandler
    {
        private readonly IVehicleRepository _repository;

        public DeleteVehicleCommandHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteVehicleCommand command)
        {
            await _repository.DeleteAsync(command.Id);
        }
    }
}