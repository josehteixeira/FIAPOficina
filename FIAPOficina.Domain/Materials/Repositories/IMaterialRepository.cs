using FIAPOficina.Domain.Materials.Entities;

namespace FIAPOficina.Domain.Materials.Repositories
{
    public interface IMaterialRepository
    {
        Task<Material> AddAsync(Material material);
        Task UpdateAsync(Material material);
        Task DeleteAsync(Guid id);
        Task<Material?> FirstOrDefaultAsync(Guid id);
        Material[] GetAll();
        Task<bool> CheckAvailability(Guid? materialId, int quantity);
    }
}
