using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;

namespace FIAPOficina.Application.Services.Commands.UpdateService
{
    internal class UpdateServiceCommandHandler
    {
        private readonly IServiceRepository _repository;

        public UpdateServiceCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Service> Handle(UpdateServiceCommand command)
        {
            var user = new Service(command.Name, command.Description, command.Value, command.Id);

            await _repository.UpdateAsync(user);

            return user;
        }
    }
}