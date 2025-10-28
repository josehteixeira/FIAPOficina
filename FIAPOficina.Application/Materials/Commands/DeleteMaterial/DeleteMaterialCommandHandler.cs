using FIAPOficina.Domain.Materials.Repositories;

namespace FIAPOficina.Application.Materials.Commands.DeleteMaterial
{
    internal class DeleteMaterialCommandHandler
    {
        private readonly IMaterialRepository _repository;

        public DeleteMaterialCommandHandler(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteMaterialCommand command)
        {
            await _repository.DeleteAsync(command.Id);
        }
    }
}