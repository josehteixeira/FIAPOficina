using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Domain.Vehicles.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);
    }
}