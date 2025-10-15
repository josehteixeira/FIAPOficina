using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
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
            var vehicleToUpdate = _context.Vehicles.FirstOrDefault(v => v.Id == vehicle.Id);

            if (vehicleToUpdate is not null)
            {
                vehicleToUpdate.Brand = vehicle.Brand;
                vehicleToUpdate.Model = vehicle.Model;
                vehicleToUpdate.Year = vehicle.Year;
                vehicleToUpdate.Plate = vehicle.Plate;
                vehicleToUpdate.Color = vehicle.Color;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientToDelete = _context.Vehicles.FirstOrDefault(c => c.Id == id);

            if (clientToDelete is not null)
            {
                _context.Vehicles.Remove(clientToDelete);
            }

            await _context.SaveChangesAsync();
        }
    }
}