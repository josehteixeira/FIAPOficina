using FIAPOficina.Domain.Services.Repositories;

namespace FIAPOficina.Application.Services.Commands.DeleteService
{
    internal class DeleteServiceCommandHandler
    {
        private readonly IServiceRepository _repository;

        public DeleteServiceCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteServiceCommand command)
        {
            await _repository.DeleteAsync(command.Id);
        }
    }
}