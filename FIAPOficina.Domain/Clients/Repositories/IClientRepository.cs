using FIAPOficina.Domain.Clients.Entities;

namespace FIAPOficina.Domain.Clients.Repositories
{
    public interface IClientRepository
    {
        Task<Client> AddAsync(Client user);
        Task UpdateAsync(Client user);
        Task DeleteAsync(Client user);
    }
}