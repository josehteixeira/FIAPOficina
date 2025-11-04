using FIAPOficina.Domain.Clients.Repositories;

namespace FIAPOficina.Application.Clients.Commands.DeleteClient
{
    internal class DeleteClientCommandHandler
    {
        private readonly IClientRepository _repository;

        public DeleteClientCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteClientCommand command)
        {
            await _repository.DeleteAsync(command.Id).ConfigureAwait(false);
        }
    }
}