using FIAPOficina.Application.Materials.Commands.CreateMaterial;
using FIAPOficina.Application.Materials.Commands.DeleteMaterial;
using FIAPOficina.Application.Materials.Commands.GetAllMaterials;
using FIAPOficina.Application.Materials.Commands.GetSingleMaterial;
using FIAPOficina.Application.Materials.Commands.UpdateMaterial;
using FIAPOficina.Domain.Materials.Entities;

namespace FIAPOficina.Application.Materials.Services
{
    public interface IMaterialsService
    {
        public Task<Material> AddAsync(CreateMaterialCommand command);
        public Task<Material> UpdateAsync(UpdateMaterialCommand command);
        public Task DeleteAsync(DeleteMaterialCommand command);
        public Task<Material?> GetSingleAsync(GetSingleMaterialCommand command);
        public Material[] GetAll(GetAllMaterialsCommand command);
    }
}
