using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Domain.Vehicles.Repositories
{
    internal interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle user);
        Task UpdateAsync(Vehicle user);
        Task DeleteAsync(Vehicle user);
    }
}