using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrder
{
    internal class StartServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IMaterialsService _materialsService;

        public StartServiceOrderCommandHandler(IServiceOrderRepository repository, IMaterialsService materialsService)
        {
            _repository = repository;
            _materialsService = materialsService;
        }

        public async Task Handle(StartServiceOrderCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.Approved)
            {
                throw new Exception("Only service orders with status \"Approved\" can be set as \"Running\"!");
            }

            var materials = _materialsService.GetAll(new(serviceOrder.Materials.Select(m => m.MaterialId).ToArray()));
            CheckMaterials(serviceOrder, materials);
            await UpdateMaterials(serviceOrder, materials);

            serviceOrder.Status = ServiceOrderStatus.Running;
            _repository.Update(serviceOrder);
        }

        private async Task UpdateMaterials(ServiceOrder serviceOrder, Material[] materials)
        {
            foreach (var material in materials)
            {
                var serviceMaterial = serviceOrder.Materials.First(m => m.MaterialId == material.Id);

                await _materialsService.UpdateAsync(new(
                    Id: material.Id,
                    Name: material.Name,
                    Description: material.Description,
                    Brand: material.Brand,
                    Value: material.Value,
                    Quantity: material.Quantity - serviceMaterial.Quantity
                )).ConfigureAwait(false);
            }
        }

        private void CheckMaterials(ServiceOrder serviceOrder, Material[] materials)
        {
            foreach (var serviceMaterial in serviceOrder.Materials)
            {
                var material = materials.FirstOrDefault(m => m.Id == serviceMaterial.MaterialId);

                if (material is null)
                    throw new Exception("Material not found!");

                if (material.Quantity < serviceMaterial.Quantity)
                {
                    throw new Exception($"There is not enough materials of '{material.Name}'!");
                }
            }
        }

        private async Task<ServiceOrder> GetServiceOrder(Guid id)
        {
            var oldServiceOrder = await _repository.FirstOrDefaultAsync(id).ConfigureAwait(false);

            if (oldServiceOrder is null)
            {
                throw new Exception("Service order not found!");
            }

            return oldServiceOrder;
        }
    }
}