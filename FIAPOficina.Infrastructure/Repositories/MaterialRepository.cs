using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Materials.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Infrastructure.Repositories
{
    internal class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;

        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<Material> AddAsync(Material material)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckAvailability(Guid? materialId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Material?> FirstOrDefaultAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Material[] GetAll()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Material material)
        {
            throw new NotImplementedException();
        }
    }
}
