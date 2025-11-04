using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder
{
    internal class CreateServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IVehiclesService _vehiclesService;
        private readonly IMaterialsService _materialsService;
        private readonly IServicesService _servicesService;

        public CreateServiceOrderCommandHandler(
            IServiceOrderRepository repository,
            IVehiclesService vehiclesService,
            IMaterialsService materialsService,
            IServicesService servicesService)
        {
            _repository = repository;
            _vehiclesService = vehiclesService;
            _materialsService = materialsService;
            _servicesService = servicesService;
        }

        public async Task<ServiceOrder> Handle(CreateServiceOrderCommand command)
        {
            var vehicle = await _vehiclesService.GetSingleAsync(new(command.VehicleId)).ConfigureAwait(false);

            if (vehicle is null)
            {
                throw new Exception("Vehicle not found!");
            }

            Guid serviceOrderId = Guid.NewGuid();
            var materials = GetServiceOrderMaterials(command.Materials, serviceOrderId);
            var services = GetServiceOrderServices(command.Services, serviceOrderId);

            ServiceOrder serviceOrder = new(vehicleId: command.VehicleId, id: serviceOrderId)
            {
                Materials = materials,
                Services = services,
                Status = ServiceOrderStatus.Received,
                CreatedOn = DateTime.UtcNow,
            };

            var createdServiceOrder = await _repository.AddAsync(serviceOrder).ConfigureAwait(false);

            return createdServiceOrder;
        }

        private List<ServiceOrderService> GetServiceOrderServices(List<ServiceOrderServiceToCreate> services, Guid serviceOrderId)
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
                    unitValue: serviceRecord.Value));
            }

            return serviceOrderServices;
        }

        private List<ServiceOrderMaterial> GetServiceOrderMaterials(List<ServiceOrderMaterialToCreate> materials, Guid serviceOrderId)
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
                    unitValue: materialRecord.Value));
            }

            return serviceOrderMaterials;
        }
    }
}