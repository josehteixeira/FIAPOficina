using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;

namespace FIAPOficina.Application.Services.Commands.CreateService
{
    internal class CreateServiceCommandHandler
    {
        private readonly IServiceRepository _repository;

        public CreateServiceCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Service> Handle(CreateServiceCommand command)
        {
            var service = await _repository.AddAsync(
                new Service(command.Name, command.Description, command.Value)
            );

            return service;
        }
    }
}