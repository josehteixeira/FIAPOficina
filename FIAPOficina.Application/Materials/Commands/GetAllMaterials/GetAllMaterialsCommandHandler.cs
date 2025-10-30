using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;

namespace FIAPOficina.Application.Materials.Commands.GetAllMaterials
{
    internal class GetAllMaterialsCommandHandler
    {

        private readonly IMaterialRepository _repository;

        public GetAllMaterialsCommandHandler(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public Material[] Handle(GetAllMaterialsCommand command)
        {
            return _repository.GetAll(command.Ids);
        }
    }
}