using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class VehiclesRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehiclesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            Vehicles createVehicles = new()
            {
                Id = Guid.NewGuid(),
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Plate = vehicle.Plate,
                Color = vehicle.Color,
                ClientId = vehicle.ClientId,
            };

            _context.Vehicles.Add(createVehicles);
            await _context.SaveChangesAsync();

            return new Vehicle(vehicle, createVehicles.Id);
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(new()
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Plate = vehicle.Plate,
                Color = vehicle.Color,
                ClientId = vehicle.ClientId,
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Vehicle vehicle)
        {
            var clientToDelete = _context.Vehicles.FirstOrDefault(c => c.Id == vehicle.Id);

            if (clientToDelete is not null)
            {
                _context.Vehicles.Remove(clientToDelete);
            }

            await _context.SaveChangesAsync();
        }
    }
}