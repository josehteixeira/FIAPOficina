using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;
using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMaterialRepository _materialRepository;

        public ServiceOrderRepository(AppDbContext context, IMaterialRepository materialRepository)
        {
            _context = context;
            _materialRepository = materialRepository;
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

        public async Task DeleteAsync(Guid id)
        {
            var serviceOrderToDelete = _context.ServiceOrders
                .Include(s => s.Materials)
                .Include(s => s.Services).FirstOrDefault();

            if (serviceOrderToDelete is not null)
            {
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    if (serviceOrderToDelete.Materials is not null && serviceOrderToDelete.Materials.Any())
                        foreach (var serviceOrderMaterial in serviceOrderToDelete.Materials)
                            _context.ServiceOrderMaterials.Remove(serviceOrderMaterial);

                    if (serviceOrderToDelete.Services is not null && serviceOrderToDelete.Services.Any())
                        foreach (var serviceOrderService in serviceOrderToDelete.Services)
                            _context.ServiceOrderServices.Remove(serviceOrderService);

                    _context.ServiceOrders.Remove(serviceOrderToDelete);
                }
                catch
                {
                    transaction.Rollback();
                }
                transaction.Commit();
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ServiceOrder?> FirstOrDefaultAsync(Guid id)
        {
            var serviceOrder = await _context.ServiceOrders
                .Include(s => s.Services)
                .Include(m => m.Materials)
                .Include(v => v.Vehicle)
                .FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);

            if (serviceOrder is not null)
            {
                return new ServiceOrder
                (
                    vehicleId: serviceOrder.VehicleId,
                    id: serviceOrder.Id
                )
                {
                    Materials = (List<ServiceOrderMaterial>)serviceOrder.Materials,
                    Services = (List<ServiceOrderService>)serviceOrder.Services,
                    Status = (ServiceOrderStatus)serviceOrder.Status

                };
            }

            return null;
        }

        public async Task<ServiceOrder?> FirstOrDefaultAsync(string plate)
        {
            var serviceOrder = await _context.ServiceOrders
               .Include(s => s.Services)
               .Include(m => m.Materials)
               .Include(v => v.Vehicle)
               .FirstOrDefaultAsync(u => u.Vehicle.Plate == plate).ConfigureAwait(false);

            if (serviceOrder is not null)
            {
                return new ServiceOrder
                (
                    vehicleId: serviceOrder.VehicleId,
                    id: serviceOrder.Id
                )
                {
                    Materials = (List<ServiceOrderMaterial>)serviceOrder.Materials,
                    Services = (List<ServiceOrderService>)serviceOrder.Services,
                    Status = (ServiceOrderStatus)serviceOrder.Status

                };
            }
            return null;
        }

        public ServiceOrder[] GetAll(Guid? vehicle)
        {
            var serviceOrders = _context.ServiceOrders.Where(s => s.VehicleId == vehicle).ToArray();
            return serviceOrders.Select(serviceOrder =>
                new ServiceOrder(serviceOrder.VehicleId)
                {
                    Id = serviceOrder.Id,
                    Materials = (List<ServiceOrderMaterial>)serviceOrder.Materials,
                    Services = (List<ServiceOrderService>)serviceOrder.Services,
                    Status = (ServiceOrderStatus)serviceOrder.Status
                }).ToArray();
        }

        public async Task RemoveMaterial(Guid serviceOrderId, Guid materialId)
        {
            var materialToDelete = _context.ServiceOrderMaterials.FirstOrDefault(c => c.MaterialId == materialId && c.ServiceOrderId == serviceOrderId);

            if (materialToDelete is not null)
            {
                _context.ServiceOrderMaterials.Remove(materialToDelete);
            }

            await _context.SaveChangesAsync();
        }
        public async Task RemoveService(Guid serviceOrderId, Guid serviceId)
        {
            var serviceToDelete = _context.ServiceOrderServices.FirstOrDefault(c => c.ServiceId == serviceId && c.ServiceOrderId == serviceOrderId);

            if (serviceToDelete is not null)
            {
                _context.ServiceOrderServices.Remove(serviceToDelete);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServiceOrder serviceOrder)
        {
            var serviceOrderToUpdate = _context.ServiceOrders.FirstOrDefault(s => s.Id == serviceOrder.Id);
            if (serviceOrderToUpdate is not null)
            {
                serviceOrderToUpdate.Status = (int)serviceOrder.Status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMaterial(Guid serviceOrderId, Guid materialId, int quantity)
        {
            var serviceOrderMaterialToUpdate = _context.ServiceOrderMaterials.FirstOrDefault(s => s.MaterialId == materialId && s.ServiceOrderId == serviceOrderId);

            if (serviceOrderMaterialToUpdate is not null)
            {
                serviceOrderMaterialToUpdate.Quantity = quantity;
                await _context.SaveChangesAsync();
            }

        }

        public Task UpdateService(Guid serviceOrderId, Guid serviceId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
