using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;

namespace FIAPOficina.Application.Tests.Mocks.Repositories
{
    internal class ServiceRepositoryMock : IServiceRepository
    {
        private List<Service> _services = new List<Service>();

        public async Task<Service> AddAsync(Service entity)
        {
            entity.Id = Guid.NewGuid();
            _services.Add(entity);
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var enity = await FirstOrDefaultAsync(id);
            if (enity is not null)
                _services.Remove(enity);
        }

        public async Task<Service?> FirstOrDefaultAsync(Guid id)
        {
            return _services.FirstOrDefault(e => e.Id == id);
        }

        public Service[] GetAll(Guid[] ids)
        {
            return _services.ToArray();
        }

        public async Task UpdateAsync(Service entity)
        {
            var update = await FirstOrDefaultAsync(entity.Id);
            update = entity;
        }
    }
}
