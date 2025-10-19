using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Vehicles.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
