using FIAPOficina.Application.Vehicles.Commands.CreateVehicle;
using FIAPOficina.Application.Vehicles.Commands.DeleteVehicle;
using FIAPOficina.Application.Vehicles.Commands.GetAllVehicles;
using FIAPOficina.Application.Vehicles.Commands.GetSingleVehicle;
using FIAPOficina.Application.Vehicles.Commands.UpdateVehicle;
using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Application.Vehicles.Services
{
    public interface IVehiclesService
    {
        public Task<Vehicle> AddAsync(CreateVehicleCommand command);
        public Task<Vehicle> UpdateAsync(UpdateVehicleCommand command);
        public Task DeleteAsync(DeleteVehicleCommand command);
        public Task<Vehicle?> GetSingleAsync(GetSingleVehicleCommand command);
        public Vehicle[] GetAll(GetAllVehiclesCommand command);
    }
}