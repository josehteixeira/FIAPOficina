using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Domain.Services.Repositories
{
    public interface IServiceRepository
    {
        Task<Service> AddAsync(Service entity);
        Task UpdateAsync(Service entity);
        Task DeleteAsync(Guid id);
        Task<Service?> FirstOrDefaultAsync(Guid id);
        Service[] GetAll(Guid[] ids);
    }
}