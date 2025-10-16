using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;

namespace FIAPOficina.Application.Clients.Commands.GetAllClients
{
    internal class GetAllClientsCommandHandler
    {
        private readonly IClientRepository _repository;

        public GetAllClientsCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public Client[] Handle(GetAllClientsCommand command)
        {
            return _repository.GetAll();
        }
    }
}