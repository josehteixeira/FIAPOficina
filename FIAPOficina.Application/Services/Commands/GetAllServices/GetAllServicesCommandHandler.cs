using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;

namespace FIAPOficina.Application.Services.Commands.GetAllServices
{
    public class GetAllServicesCommandHandler
    {
        private readonly IServiceRepository _repository;

        public GetAllServicesCommandHandler(IServiceRepository repository)
        {
            _repository = repository;
        }

        public Service[] Handle(GetAllServicesCommand command)
        {
            return _repository.GetAll(command.Ids);
        }
    }
}