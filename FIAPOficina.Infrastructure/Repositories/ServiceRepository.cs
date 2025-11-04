using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Service> AddAsync(Service service)
        {
            Services createServices = new()
            {
                Id = service.Id == Guid.Empty ? Guid.NewGuid() : service.Id,
                Name = service.Name,
                Description = service.Description,
                Value = service.Value
            };

            _context.Services.Add(createServices);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return new Service(service, createServices.Id);
        }

        public async Task UpdateAsync(Service service)
        {
            var serviceToUpdate = _context.Services.FirstOrDefault(s => s.Id == service.Id);

            if (serviceToUpdate is not null)
            {
                serviceToUpdate.Name = service.Name;
                serviceToUpdate.Description = service.Description;
                serviceToUpdate.Value = service.Value;

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var serviceToDelete = _context.Services.FirstOrDefault(c => c.Id == id);

            if (serviceToDelete is not null)
            {
                _context.Services.Remove(serviceToDelete);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Service?> FirstOrDefaultAsync(Guid id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);

            if (service is not null)
            {
                return new Service
                (
                    name: service.Name,
                    description: service.Description,
                    value: service.Value,
                    id: service.Id
                );
            }

            return null;
        }

        public Service[] GetAll(Guid[] ids)
        {
            var services = ids is null || ids.Length == 0 ?
                _context.Services.ToArray()
                : _context.Services.Where(s => ids.Contains(s.Id)).ToArray();


            return services.Select(service =>
                new Service
                (
                    service.Name,
                    service.Description,
                    service.Value,
                    service.Id
                )).ToArray();
        }
    }
}