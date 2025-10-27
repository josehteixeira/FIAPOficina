using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;
using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Infrastructure.Repositories
{
    internal class ServiceOrderRepository : IServiceOrderRespository
    {
        private readonly AppDbContext _context;

        public ServiceOrderRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ServiceOrder> AddAsync(ServiceOrder serviceOrder)
        {
            ServiceOrders createServiceOrder = new()
            {
                Id = Guid.NewGuid(),
                VehicleId = serviceOrder.VehicleId,
                Status = (int)serviceOrder.Status
            };

            _context.ServiceOrders.Add(createServiceOrder);
            await _context.SaveChangesAsync();

            return new ServiceOrder(serviceOrder, createServiceOrder.Id);
        }

        public async Task AddMaterial(Guid serviceOrderId, Material material, int quantity)
        {
            ServiceOrderMaterials createdServiceOrderMaterials = new()
            {
                Id = Guid.NewGuid(),
                MaterialId = material.Id,
                Quantity = quantity,
                ServiceOrderId = serviceOrderId,
                Value = material.Value
            };
            _context.ServiceOrderMaterials.Add(createdServiceOrderMaterials);
            await _context.SaveChangesAsync();
        }

        public async Task AddService(Guid serviceOrderId, Service service, int quantity)
        {
            ServiceOrderServices createdServiceOrderServices = new()
            {
                Id = Guid.NewGuid(),
                ServiceId = service.Id,
                Quantity = quantity,
                ServiceOrderId = serviceOrderId,
                Value = service.Value
            };
            _context.ServiceOrderServices.Add(createdServiceOrderServices);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteService(Guid serviceOrderId, Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle?> FirstOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceOrder?> FirstOrDefaultAsync(string plate)
        {
            throw new NotImplementedException();
        }

        public ServiceOrder[] GetAll(Guid? vehicle)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMaterial(Guid serviceOrderId, Guid materialId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ServiceOrder serviceOrder)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMaterial(Guid serviceOrderId, Guid materialId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateService(Guid serviceOrderId, Guid serviceId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
