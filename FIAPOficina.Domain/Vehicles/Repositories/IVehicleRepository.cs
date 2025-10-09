using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Domain.Vehicles.Repositories
{
    internal interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
    }
}