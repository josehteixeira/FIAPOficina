using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;

namespace FIAPOficina.Application.Materials.Commands.CreateMaterial
{
    internal class CreateMaterialCommandHandler
    {
        private readonly IMaterialRepository _repository;

        public CreateMaterialCommandHandler(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<Material> Handle(CreateMaterialCommand command)
        {
            var material = await _repository.AddAsync(new(
                name: command.Name,
                description: command.Description,
                brand: command.Brand,
                value: command.Value,
                quantity: command.Quantity)
            ).ConfigureAwait(false);

            return material;
        }
    }
}