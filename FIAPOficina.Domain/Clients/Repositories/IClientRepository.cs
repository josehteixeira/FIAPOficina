using FIAPOficina.Domain.Clients.Entities;

namespace FIAPOficina.Domain.Clients.Repositories
{
    public interface IClientRepository
    {
        Task<Client> AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Guid id);
        Task<Client?> FirstOrDefaultAsync(Guid id);
        Task<Client?> FirstOrDefaultAsync(string identifider);
        Client[] GetAll();
    }
}