using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;

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
                Id = Guid.NewGuid(),
                Name = service.Name,
                Description = service.Description,
                Value = service.Value
            };

            _context.Services.Add(createServices);
            await _context.SaveChangesAsync();

            return new Service(service, createServices.Id);
        }

        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(new()
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Value = service.Value
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Service service)
        {
            var clientToDelete = _context.Services.FirstOrDefault(c => c.Id == service.Id);

            if (clientToDelete is not null)
            {
                _context.Services.Remove(clientToDelete);
            }

            await _context.SaveChangesAsync();
        }
    }
}