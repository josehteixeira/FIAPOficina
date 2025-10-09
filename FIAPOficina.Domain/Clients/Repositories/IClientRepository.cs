using FIAPOficina.Domain.Clients.Entities;

namespace FIAPOficina.Domain.Clients.Repositories
{
    public interface IClientRepository
    {
        Task<Client> AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);
    }
}