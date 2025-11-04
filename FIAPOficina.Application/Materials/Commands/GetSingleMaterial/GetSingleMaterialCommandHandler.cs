using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;

namespace FIAPOficina.Application.Materials.Commands.GetSingleMaterial
{
    internal class GetSingleMaterialCommandHandler
    {
        private readonly IMaterialRepository _repository;

        public GetSingleMaterialCommandHandler(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<Material?> Handle(GetSingleMaterialCommand command)
        {
            return await _repository.FirstOrDefaultAsync(command.Id).ConfigureAwait(false);
        }
    }
}