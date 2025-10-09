using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Domain.Services.Repositories
{
    public interface IServicesRepository
    {
        Task<Service> AddAsync(Service entity);
        Task UpdateAsync(Service entity);
        Task DeleteAsync(Service entity);
    }
}