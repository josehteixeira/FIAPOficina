using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder
{
    internal class UpdateServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IMaterialsService _materialsService;
        private readonly IServicesService _servicesService;

        public UpdateServiceOrderCommandHandler(
            IServiceOrderRepository repository,
            IMaterialsService materialsService,
            IServicesService servicesService)
        {
            _repository = repository;
            _materialsService = materialsService;
            _servicesService = servicesService;
        }

        public async Task<ServiceOrder> Handle(UpdateServiceOrderCommand command)
        {
            var materials = GetServiceOrderMaterials(command.Materials, command.Id);
            var services = GetServiceOrderServices(command.Services, command.Id);

            var serviceOrder = new ServiceOrder(command.VehicleId, command.Id)
            {
                Materials = materials,
                Services = services,
                Status = command.Status,
            };

            await _repository.UpdateAsync(serviceOrder).ConfigureAwait(false);

            return serviceOrder;
        }

        private List<ServiceOrderService> GetServiceOrderServices(List<ServiceOrderServiceToUpdate> services, Guid serviceOrderId)
        {
            var allServices = _servicesService.GetAll(new(services.Select(m => m.ServiceId).ToArray()));
            List<ServiceOrderService> serviceOrderServices = new List<ServiceOrderService>();

            foreach (var service in services)
            {
                var serviceRecord = allServices.FirstOrDefault(m => m.Id == service.ServiceId);

                if (serviceRecord is null)
                    throw new Exception("Service not found!");

                serviceOrderServices.Add(new(
                    serviceId: service.ServiceId,
                    serviceOrderId: serviceOrderId,
                    quantity: service.Quantity,
                    unitValue: serviceRecord.Value,
                    id: service.Id));
            }

            return serviceOrderServices;
        }

        private List<ServiceOrderMaterial> GetServiceOrderMaterials(List<ServiceOrderMaterialToUpdate> materials, Guid serviceOrderId)
        {
            var allMaterials = _materialsService.GetAll(new(materials.Select(m => m.MaterialId).ToArray()));
            List<ServiceOrderMaterial> serviceOrderMaterials = new List<ServiceOrderMaterial>();

            foreach (var material in materials)
            {
                var materialRecord = allMaterials.FirstOrDefault(m => m.Id == material.MaterialId);

                if (materialRecord is null)
                    throw new Exception("Material not found!");

                serviceOrderMaterials.Add(new(
                    materialId: material.MaterialId,
                    serviceOrderId: serviceOrderId,
                    quantity: material.Quantity,
                    unitValue: materialRecord.Value,
                    id: material.Id));
            }

            return serviceOrderMaterials;
        }
    }
}