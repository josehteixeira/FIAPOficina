using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;

namespace FIAPOficina.Application.Vehicles.Commands.GetSingleVehicle
{
    internal class GetSingleVehicleCommandHandler
    {
        private readonly IVehicleRepository _repository;

        public GetSingleVehicleCommandHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Vehicle?> Handle(GetSingleVehicleCommand command)
        {
            if (string.IsNullOrEmpty(command.Plate))
            {
                return await _repository.FirstOrDefaultAsync(command.Id);
            }
            return await _repository.FirstOrDefaultAsync(command.Plate);
        }
    }
}