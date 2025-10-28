using FIAPOficina.Application.Materials.Commands.CreateMaterial;
using FIAPOficina.Application.Materials.Commands.DeleteMaterial;
using FIAPOficina.Application.Materials.Commands.GetAllMaterials;
using FIAPOficina.Application.Materials.Commands.GetSingleMaterial;
using FIAPOficina.Application.Materials.Commands.UpdateMaterial;
using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;

namespace FIAPOficina.Application.Materials.Services
{
    public class MaterialsService : IMaterialsService
    {
        private readonly CreateMaterialCommandHandler _createHandler;
        private readonly UpdateMaterialCommandHandler _updateHandler;
        private readonly DeleteMaterialCommandHandler _deleteHandler;
        private readonly GetSingleMaterialCommandHandler _querySingleHandler;
        private readonly GetAllMaterialsCommandHandler _queryAllHandler;

        public MaterialsService(IMaterialRepository repository)
        {
            _createHandler = new CreateMaterialCommandHandler(repository);
            _updateHandler = new UpdateMaterialCommandHandler(repository);
            _deleteHandler = new DeleteMaterialCommandHandler(repository);
            _querySingleHandler = new GetSingleMaterialCommandHandler(repository);
            _queryAllHandler = new GetAllMaterialsCommandHandler(repository);
        }
        public async Task<Material> AddAsync(CreateMaterialCommand command)
        {
            return await _createHandler.Handle(command);
        }

        public async Task DeleteAsync(DeleteMaterialCommand command)
        {
            await _deleteHandler.Handle(command);
        }

        public Material[] GetAll(GetAllMaterialsCommand command)
        {
            return _queryAllHandler.Handle(command);
        }

        public async Task<Material?> GetSingleAsync(GetSingleMaterialCommand command)
        {
            return await _querySingleHandler.Handle(command).ConfigureAwait(false);
        }

        public Task<Material> UpdateAsync(UpdateMaterialCommand command)
        {
            return _updateHandler.Handle(command);
        }
    }
}