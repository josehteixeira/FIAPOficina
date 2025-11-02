using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;

namespace FIAPOficina.Application.Tests.Mocks.Repositories
{
    internal class MaterialRepositoryMock : IMaterialRepository
    {
        private List<Material> _materials = new List<Material>();
        public async Task<Material> AddAsync(Material material)
        {
            material.Id = Guid.NewGuid();
            _materials.Add(material);
            return material;
        }

        public Task ChangeQuantity(Guid? materialId, int quantity)
        {
            var material = _materials.FirstOrDefault(m => m.Id == materialId);
            if (material != null)
                material.Quantity = quantity;

            return Task.CompletedTask;

        }

        public Task<bool> CheckAvailability(Guid? materialId, int quantity)
        {
            var material = _materials.FirstOrDefault(m => m.Id == materialId);
            return Task.FromResult(material != null && material.Quantity >= quantity);

        }

        public Task DeleteAsync(Guid id)
        {
            var material = _materials.FirstOrDefault(m => m.Id == id);
            if (material != null)
                _materials.Remove(material);

            return Task.CompletedTask;
        }

        public Task<Material?> FirstOrDefaultAsync(Guid id)
        {
            return Task.FromResult(_materials.FirstOrDefault(m => m.Id == id));
        }

        public Material[] GetAll(Guid[] ids)
        {
            return _materials.Where(m => ids.Contains(m.Id)).ToArray();
        }

        public async Task UpdateAsync(Material material)
        {
            var materiadb = FirstOrDefaultAsync(material.Id).GetAwaiter().GetResult();
            if (materiadb is not null)
            {
                materiadb.Quantity = material.Quantity;
                materiadb.Brand = material.Brand;
                materiadb.Value = material.Value;
                materiadb.Description = material.Description;
                materiadb.Name = material.Name;
            }
        }
    }
}
