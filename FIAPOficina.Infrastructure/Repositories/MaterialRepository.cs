using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;

        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Material> AddAsync(Material material)
        {
            Materials createMaterial = new()
            {
                Id = Guid.NewGuid(),
                Name = material.Name,
                Description = material.Description,
                Value = material.Value,
                Quantity = material.Quantity,
                Brand = material.Brand,
            };
            _context.Materials.Add(createMaterial);
            await _context.SaveChangesAsync();

            return new Material(material, createMaterial.Id);
        }

        public Task<bool> CheckAvailability(Guid? materialId, int quantity)
        {

            return Task.FromResult(_context.Materials.FirstOrDefault(m => m.Id == materialId && m.Quantity >= quantity) != null);
        }

        public async Task DeleteAsync(Guid id)
        {
            var materialToDelete = _context.Materials.FirstOrDefault(c => c.Id == id);

            if (materialToDelete is not null)
            {
                _context.Materials.Remove(materialToDelete);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Material?> FirstOrDefaultAsync(Guid id)
        {
            var material = await _context.Materials.FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);

            if (material is not null)
            {
                return new Material
                (
                    name: material.Name,
                    description: material.Description,
                    brand: material.Brand,
                    value: material.Value,
                    id: material.Id,
                    quantity: material.Quantity
                );
            }

            return null;
        }

        public Material[] GetAll()
        {
            var materials = _context.Materials.ToArray();

            return materials.Select(material =>
                new Material
                (
                    material.Name,
                    material.Description,
                    material.Brand,
                    material.Value,
                    material.Id,
                    material.Quantity
                )).ToArray();
        }

        public async Task ChangeQuantity(Guid? materialId, int quantity)
        {
            var materialToUpdate = _context.Materials.FirstOrDefault(s => s.Id == materialId);

            if (materialToUpdate is not null)
            {
                materialToUpdate.Quantity += quantity;

                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Material material)
        {
            var materialToUpdate = _context.Materials.FirstOrDefault(s => s.Id == material.Id);

            if (materialToUpdate is not null)
            {
                materialToUpdate.Name = material.Name;
                materialToUpdate.Description = material.Description;
                materialToUpdate.Brand = material.Brand;
                materialToUpdate.Value = material.Value;
                materialToUpdate.Quantity = material.Quantity;

                await _context.SaveChangesAsync();
            }
        }
    }
}
