using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class ServiceOrderRepository : IServiceOrderRepository
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
                Id = serviceOrder.Id == Guid.Empty ? Guid.NewGuid() : serviceOrder.Id,
                VehicleId = serviceOrder.VehicleId,
                Status = (int)serviceOrder.Status,
                CreatedOn = serviceOrder.CreatedOn,
            };

            using (var transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false))
            {
                try
                {
                    AddMaterials(serviceOrder.Materials, createServiceOrder.Id);
                    AddAllServices(serviceOrder.Services, createServiceOrder.Id);

                    _context.ServiceOrders.Add(createServiceOrder);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return new ServiceOrder(serviceOrder, createServiceOrder.Id);
        }

        private void AddMaterials(List<ServiceOrderMaterial> serviceOrderMaterials, Guid id)
        {
            if (serviceOrderMaterials is not null && serviceOrderMaterials.Any())
            {
                foreach (var material in serviceOrderMaterials)
                {
                    _context.ServiceOrderMaterials.Add(new()
                    {
                        Id = material.Id == Guid.Empty ? Guid.NewGuid() : material.Id,
                        ServiceOrderId = id,
                        MaterialId = material.MaterialId,
                        Quantity = material.Quantity,
                        Value = material.UnitValue,
                    });
                }
            }
        }

        private void AddAllServices(List<ServiceOrderService> serviceOrderServices, Guid id)
        {
            if (serviceOrderServices is not null && serviceOrderServices.Any())
            {
                foreach (var service in serviceOrderServices)
                {
                    _context.ServiceOrderServices.Add(new()
                    {
                        Id = service.Id == Guid.Empty ? Guid.NewGuid() : service.Id,
                        ServiceOrderId = id,
                        ServiceId = service.ServiceId,
                        Quantity = service.Quantity,
                        Value = service.UnitValue,
                    });
                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var serviceOrderToDelete = await _context.ServiceOrders
                .Include(s => s.Materials)
                .Include(s => s.Services)
                .FirstOrDefaultAsync(so => so.Id == id)
                .ConfigureAwait(false);

            if (serviceOrderToDelete is not null)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    try
                    {
                        if (serviceOrderToDelete.Materials is not null && serviceOrderToDelete.Materials.Any())
                            foreach (var serviceOrderMaterial in serviceOrderToDelete.Materials)
                                _context.ServiceOrderMaterials.Remove(serviceOrderMaterial);

                        if (serviceOrderToDelete.Services is not null && serviceOrderToDelete.Services.Any())
                            foreach (var serviceOrderService in serviceOrderToDelete.Services)
                                _context.ServiceOrderServices.Remove(serviceOrderService);

                        _context.ServiceOrders.Remove(serviceOrderToDelete);
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task<ServiceOrder?> FirstOrDefaultAsync(Guid id)
        {
            var serviceOrder = await _context.ServiceOrders
                .Include(so => so.Services)
                .Include(so => so.Materials)
                .Include(so => so.Vehicle)
                .FirstOrDefaultAsync(so => so.Id == id)
                .ConfigureAwait(false);

            if (serviceOrder is not null)
            {
                return new ServiceOrder(serviceOrder.VehicleId, id: serviceOrder.Id)
                {
                    CreatedOn = serviceOrder.CreatedOn,
                    ApprovedOn = serviceOrder.ApprovedOn,
                    FinishedOn = serviceOrder.FinishedOn,
                    Materials = serviceOrder.Materials.Select(m => new ServiceOrderMaterial(m.MaterialId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Services = serviceOrder.Services.Select(m => new ServiceOrderService(m.ServiceId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Status = (ServiceOrderStatus)serviceOrder.Status
                };
            }

            return null;
        }

        public async Task<ServiceOrder?> FirstOrDefaultAsync(string plate)
        {
            var serviceOrder = await _context.ServiceOrders
               .Include(so => so.Services)
               .Include(so => so.Materials)
               .Include(so => so.Vehicle)
               .FirstOrDefaultAsync(v => v.Vehicle.Plate == plate)
               .ConfigureAwait(false);

            if (serviceOrder is not null)
            {
                return new ServiceOrder(serviceOrder.VehicleId, id: serviceOrder.Id)
                {
                    CreatedOn = serviceOrder.CreatedOn,
                    ApprovedOn = serviceOrder.ApprovedOn,
                    FinishedOn = serviceOrder.FinishedOn,
                    Materials = serviceOrder.Materials.Select(m => new ServiceOrderMaterial(m.MaterialId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Services = serviceOrder.Services.Select(m => new ServiceOrderService(m.ServiceId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Status = (ServiceOrderStatus)serviceOrder.Status
                };
            }
            return null;
        }

        public ServiceOrder[] GetAll(Guid? vehicleId = null, Guid? clientId = null)
        {
            var serviceOrders = _context.ServiceOrders
                .Include(so => so.Materials)
                .Include(so => so.Services)
                .Where(so => !vehicleId.HasValue || so.VehicleId == vehicleId)
                .Where(so => !clientId.HasValue || so.Vehicle.ClientId == clientId)
                .ToArray();

            return serviceOrders.Select(serviceOrder =>
                new ServiceOrder(serviceOrder.VehicleId)
                {
                    Id = serviceOrder.Id,
                    CreatedOn = serviceOrder.CreatedOn,
                    ApprovedOn = serviceOrder.ApprovedOn,
                    FinishedOn = serviceOrder.FinishedOn,
                    Materials = serviceOrder.Materials.Select(m => new ServiceOrderMaterial(m.MaterialId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Services = serviceOrder.Services.Select(m => new ServiceOrderService(m.ServiceId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Status = (ServiceOrderStatus)serviceOrder.Status
                }).ToArray();
        }

        public void Update(ServiceOrder serviceOrder)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var serviceOrderToUpdate = _context.ServiceOrders
                .Include(so => so.Materials)
                .Include(so => so.Services)
                .FirstOrDefault(so => so.Id == serviceOrder.Id);

                if (serviceOrderToUpdate is not null)
                {
                    try
                    {
                        serviceOrderToUpdate.Status = (int)serviceOrder.Status;
                        serviceOrderToUpdate.VehicleId = serviceOrder.VehicleId;
                        serviceOrderToUpdate.CreatedOn = serviceOrder.CreatedOn;
                        serviceOrderToUpdate.ApprovedOn = serviceOrder.ApprovedOn;
                        serviceOrderToUpdate.FinishedOn = serviceOrder.FinishedOn;

                        UpdateServiceOrderMaterials(serviceOrderToUpdate, serviceOrder);
                        UpdateServiceOrderServices(serviceOrderToUpdate, serviceOrder);

                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void UpdateServiceOrderMaterials(ServiceOrders oldServiceOrder, ServiceOrder newServiceOrder)
        {
            var oldMaterialsList = oldServiceOrder.ToDomain().Materials.AsEnumerable();
            var newMaterialsList = newServiceOrder.Materials.AsEnumerable();

            var materialsToAdd = newMaterialsList.Where(n => !oldMaterialsList.Any(o => o.Id == n.Id));
            var materialsToDelete = oldMaterialsList.Where(o => !newMaterialsList.Any(n => n.Id == o.Id));
            var materialsToUpdate = newMaterialsList.Where(o => oldMaterialsList.Any(n => n.Id == o.Id));

            AddMaterials(materialsToAdd.ToList(), newServiceOrder.Id);
            DeleteMaterials(materialsToDelete);
            UpdateMaterials(materialsToUpdate);
        }

        private void UpdateMaterials(IEnumerable<ServiceOrderMaterial> materialsToUpdate)
        {

            foreach (var materialToUpdate in materialsToUpdate)
            {
                var material = _context.ServiceOrderMaterials.FirstOrDefault(m => m.Id == materialToUpdate.Id);

                if (material is not null)
                {
                    material.Value = materialToUpdate.UnitValue;
                    material.Quantity = materialToUpdate.Quantity;
                    material.MaterialId = materialToUpdate.MaterialId;
                    _context.ServiceOrderMaterials.Update(material);
                }
            }
        }

        private void DeleteMaterials(IEnumerable<ServiceOrderMaterial> materialsToDelete)
        {
            foreach (var materialToDelete in materialsToDelete)
            {
                var material = _context.ServiceOrderMaterials.FirstOrDefault(m => m.Id == materialToDelete.Id);

                if (material is not null)
                {
                    _context.ServiceOrderMaterials.Remove(material);
                }
            }
        }

        private void UpdateServiceOrderServices(ServiceOrders oldServiceOrder, ServiceOrder newServiceOrder)
        {
            var oldServicesList = oldServiceOrder.ToDomain().Services.AsEnumerable();
            var newServicesList = newServiceOrder.Services.AsEnumerable();

            var servicesToAdd = newServicesList.Where(n => !oldServicesList.Any(o => o.Id == n.Id));
            var servicesToDelete = oldServicesList.Where(o => !newServicesList.Any(n => n.Id == o.Id));
            var servicesToUpdate = newServicesList.Where(o => oldServicesList.Any(n => n.Id == o.Id));

            AddAllServices(servicesToAdd.ToList(), newServiceOrder.Id);
            DeleteAllServices(servicesToDelete);
            UpdateAllServices(servicesToUpdate);
        }

        private void DeleteAllServices(IEnumerable<ServiceOrderService> servicesToDelete)
        {
            foreach (var serviceToDelete in servicesToDelete)
            {
                var service = _context.ServiceOrderServices.FirstOrDefault(m => m.Id == serviceToDelete.Id);

                if (service is not null)
                {
                    _context.ServiceOrderServices.Remove(service);
                }
            }
        }

        private void UpdateAllServices(IEnumerable<ServiceOrderService> servicesToUpdate)
        {

            foreach (var serviceToUpdate in servicesToUpdate)
            {
                var service = _context.ServiceOrderServices.FirstOrDefault(m => m.Id == serviceToUpdate.Id);

                if (service is not null)
                {
                    service.Value = serviceToUpdate.UnitValue;
                    service.Quantity = serviceToUpdate.Quantity;
                    service.ServiceId = serviceToUpdate.ServiceId;
                    _context.ServiceOrderServices.Update(service);
                }
            }
        }

        public ServiceOrder[] GetServicesOrderByVehiclePlate(string vehiclePlate)
        {
            var serviceOrders = _context.ServiceOrders
                .Include(so => so.Materials)
                .Include(so => so.Services)
                .Include(so => so.Vehicle)
                .Where(so => so.Vehicle.Plate == vehiclePlate)
                .ToArray();

            return serviceOrders.Select(serviceOrder =>
                new ServiceOrder(serviceOrder.VehicleId)
                {
                    Id = serviceOrder.Id,
                    CreatedOn = serviceOrder.CreatedOn,
                    ApprovedOn = serviceOrder.ApprovedOn,
                    FinishedOn = serviceOrder.FinishedOn,
                    Materials = serviceOrder.Materials.Select(m => new ServiceOrderMaterial(m.MaterialId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Services = serviceOrder.Services.Select(m => new ServiceOrderService(m.ServiceId, serviceOrder.Id, m.Quantity, m.Value, m.Id)).ToList(),
                    Status = (ServiceOrderStatus)serviceOrder.Status
                }).ToArray();
        }
    }

    static class Extensions
    {
        public static ServiceOrder ToDomain(this ServiceOrders serviceOrder)
        {
            return new ServiceOrder(serviceOrder.VehicleId, id: serviceOrder.Id)
            {
                Materials = serviceOrder.Materials.Select(m => m.ToDomain()).ToList(),
                Services = serviceOrder.Services.Select(s => s.ToDomain()).ToList(),
                Status = (ServiceOrderStatus)serviceOrder.Status,
            };
        }

        public static ServiceOrderMaterial ToDomain(this ServiceOrderMaterials serviceOrderMaterial)
        {
            return new(
                id: serviceOrderMaterial.Id,
                materialId: serviceOrderMaterial.MaterialId,
                serviceOrderId: serviceOrderMaterial.ServiceOrderId,
                unitValue: serviceOrderMaterial.Value,
                quantity: serviceOrderMaterial.Quantity
            );
        }

        public static ServiceOrderService ToDomain(this ServiceOrderServices serviceOrderService)
        {
            return new(
                id: serviceOrderService.Id,
                serviceId: serviceOrderService.ServiceId,
                serviceOrderId: serviceOrderService.ServiceOrderId,
                unitValue: serviceOrderService.Value,
                quantity: serviceOrderService.Quantity
            );
        }
    }
}