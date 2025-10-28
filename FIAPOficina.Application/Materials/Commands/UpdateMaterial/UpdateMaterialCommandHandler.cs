using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;

namespace FIAPOficina.Application.Materials.Commands.UpdateMaterial
{
    internal class UpdateMaterialCommandHandler
    {
        private readonly IMaterialRepository _repository;

        public UpdateMaterialCommandHandler(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<Material> Handle(UpdateMaterialCommand command)
        {
            Material material = new(
               name: command.Name,
               description: command.Description,
               brand: command.Brand,
               value: command.Value,
               quantity: command.Quantity,
               id: command.Id);

            await _repository.UpdateAsync(material).ConfigureAwait(false);

            return material;
        }
    }
}