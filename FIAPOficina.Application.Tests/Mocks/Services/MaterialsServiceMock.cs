using FIAPOficina.Application.Materials.Commands.CreateMaterial;
using FIAPOficina.Application.Materials.Commands.DeleteMaterial;
using FIAPOficina.Application.Materials.Commands.GetAllMaterials;
using FIAPOficina.Application.Materials.Commands.GetSingleMaterial;
using FIAPOficina.Application.Materials.Commands.UpdateMaterial;
using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Domain.Materials.Entities;

namespace FIAPOficina.Application.Tests.Mocks.Services
{
    internal class MaterialsServiceMock : IMaterialsService
    {
        public Task<Material> AddAsync(CreateMaterialCommand command)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DeleteMaterialCommand command)
        {
            throw new NotImplementedException();
        }

        public Material[] GetAll(GetAllMaterialsCommand command)
        {
            var materials = new Material[1];
            materials[0] = new Material("", "", "", 1, 10,Guid.Parse("CE91D1FC-DBF1-4AB1-9D10-F69C25E10C5B"));

            return materials;
        }

        public Task<Material?> GetSingleAsync(GetSingleMaterialCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<Material> UpdateAsync(UpdateMaterialCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
