using FIAPOficina.Application.Vehicles.Commands.CreateVehicle;
using FIAPOficina.Application.Vehicles.Commands.DeleteVehicle;
using FIAPOficina.Application.Vehicles.Commands.GetAllVehicles;
using FIAPOficina.Application.Vehicles.Commands.GetSingleVehicle;
using FIAPOficina.Application.Vehicles.Commands.UpdateVehicle;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.Vehicles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Mocks.Services
{
    internal class VehicleServiceMock : IVehiclesService
    {
        public Task<Vehicle> AddAsync(CreateVehicleCommand command)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DeleteVehicleCommand command)
        {
            throw new NotImplementedException();
        }

        public Vehicle[] GetAll(GetAllVehiclesCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle?> GetSingleAsync(GetSingleVehicleCommand command)
        {
            return new Vehicle("","",2020,"QHH8H99","",Guid.NewGuid());
        }

        public Task<Vehicle> UpdateAsync(UpdateVehicleCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
