using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;

namespace FIAPOficina.Application.Vehicles.Commands.UpdateVehicle
{
    internal class UpdateVehicleCommandHandler
    {
        private readonly IVehicleRepository _repository;

        public UpdateVehicleCommandHandler(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Vehicle> Handle(UpdateVehicleCommand command)
        {
            var vehicle = await _repository.FirstOrDefaultAsync(command.Id).ConfigureAwait(false);

            if (vehicle is null)
            {
                throw new Exception("Vehicle not found!");
            }

            vehicle.Brand = command.Brand;
            vehicle.Model = command.Model;
            vehicle.Year = command.Year;
            vehicle.Plate = command.Plate;
            vehicle.Color = command.Color;

            await _repository.UpdateAsync(vehicle).ConfigureAwait(false);

            return vehicle;
        }
    }
}