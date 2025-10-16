using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Domain.Vehicles.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Guid id);
        Task<Vehicle?> FirstOrDefaultAsync(Guid id);
        Task<Vehicle?> FirstOrDefaultAsync(string plate);
        Vehicle[] GetAll(Guid? clientId);
    }
}