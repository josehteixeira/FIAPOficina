using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;

namespace FIAPOficina.Application.Services.Commands.GetSingleService
{
    internal class GetSingleServiceCommandHandler
    {
        private readonly IServiceRepository _repository;

        public GetSingleServiceCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Service?> Handle(GetSingleServiceCommand command)
        {
            return await _repository.FirstOrDefaultAsync(command.Id);
        }
    }
}