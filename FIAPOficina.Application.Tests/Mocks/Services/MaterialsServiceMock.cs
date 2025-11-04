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
        private List<Material> _materials = new List<Material>();

        public Task<Material> AddAsync(CreateMaterialCommand command)
        {
            Material material = new(command.Name, command.Description, command.Brand, command.Value, command.Quantity);
            _materials.Add(material);

            return Task.FromResult(material);
        }

        public Task DeleteAsync(DeleteMaterialCommand command)
        {
            throw new NotImplementedException();
        }

        public Material[] GetAll(GetAllMaterialsCommand command)
        {

            _materials.Add(new Material("", "", "", 1, 10, Guid.Parse("CE91D1FC-DBF1-4AB1-9D10-F69C25E10C5B")));

            return _materials.Where(m => command.Ids.Contains(m.Id)).ToArray();
        }

        public Task<Material?> GetSingleAsync(GetSingleMaterialCommand command)
        {
            return Task.FromResult(_materials.Where(m => command.Id == m.Id).FirstOrDefault());
        }

        public Task<Material> UpdateAsync(UpdateMaterialCommand command)
        {
            Material material = _materials.First(m => m.Id == command.Id);
            _materials.Remove(material);

            material.Name = command.Name;
            material.Description = command.Description;
            material.Brand = command.Brand;
            material.Value = command.Value;
            material.Quantity = command.Quantity;

            _materials.Add(material);

            return Task.FromResult(material);
        }
    }
}
