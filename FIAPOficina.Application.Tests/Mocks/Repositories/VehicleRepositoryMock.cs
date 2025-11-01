using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Mocks.Repositories
{
    internal class VehicleRepositoryMock : IVehicleRepository
    {
        private List<Vehicle> _vehicles= new List<Vehicle>();
        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            _vehicles.Add(vehicle);
            return vehicle;
        }

        public async Task DeleteAsync(Guid id)
        {
            var vehicle = FirstOrDefaultAsync(id).GetAwaiter().GetResult();
            _vehicles.Remove(vehicle);
        }

        public async Task<Vehicle?> FirstOrDefaultAsync(Guid id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        public async Task<Vehicle?> FirstOrDefaultAsync(string plate)
        {
            return _vehicles.FirstOrDefault(v => v.Plate.Equals(plate));
        }

        public Vehicle[] GetAll(Guid? clientId)
        {
            return _vehicles.ToArray();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            var vehicleDb = FirstOrDefaultAsync(vehicle.Id).GetAwaiter().GetResult();
            if (vehicleDb is not null)
            {
                vehicleDb.Plate = vehicle.Plate;
                vehicleDb.Year = vehicle.Year;  
                vehicleDb.Brand = vehicle.Brand;    
                vehicleDb.Color = vehicle.Color;
                vehicleDb.ClientId= vehicle.ClientId;
            }
        }
    }
}
