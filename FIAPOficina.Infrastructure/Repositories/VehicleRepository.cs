using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

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
            Vehicles createVehicle = new()
            {
                Id = vehicle.Id == Guid.Empty ? Guid.NewGuid() : vehicle.Id,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Plate = vehicle.Plate,
                Color = vehicle.Color,
                ClientId = vehicle.ClientId,
            };

            _context.Vehicles.Add(createVehicle);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new Vehicle(vehicle, createVehicle.Id);
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

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var vehicleToDelete = _context.Vehicles.FirstOrDefault(c => c.Id == id);

            if (vehicleToDelete is not null)
            {
                _context.Vehicles.Remove(vehicleToDelete);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Vehicle?> FirstOrDefaultAsync(Guid id)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id).ConfigureAwait(false);

            if (vehicle is not null)
            {
                return new Vehicle
                (
                    brand: vehicle.Brand,
                    model: vehicle.Model,
                    year: vehicle.Year,
                    plate: vehicle.Plate,
                    color: vehicle.Color,
                    clientId: vehicle.ClientId,
                    id: vehicle.Id
                );
            }

            return null;
        }

        public async Task<Vehicle?> FirstOrDefaultAsync(string plate)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.Plate == plate).ConfigureAwait(false);

            if (vehicle is not null)
            {
                return new Vehicle
                (
                    brand: vehicle.Brand,
                    model: vehicle.Model,
                    year: vehicle.Year,
                    plate: vehicle.Plate,
                    color: vehicle.Color,
                    clientId: vehicle.ClientId,
                    id: vehicle.Id
                );
            }

            return null;
        }

        public Vehicle[] GetAll(Guid? clientId)
        {
            var vehicles = _context.Vehicles.Where(v => !clientId.HasValue ? true : v.ClientId == clientId);

            return vehicles.Select(vehicle =>
                new Vehicle
                (
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.Year,
                    vehicle.Plate,
                    vehicle.Color,
                    vehicle.ClientId,
                    vehicle.Id
                )).ToArray();
        }
    }
}