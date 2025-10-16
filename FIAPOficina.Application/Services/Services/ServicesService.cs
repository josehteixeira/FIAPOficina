using FIAPOficina.Application.Common.Security;
using FIAPOficina.Application.Services.Commands.CreateService;
using FIAPOficina.Application.Services.Commands.DeleteService;
using FIAPOficina.Application.Services.Commands.GetAllServices;
using FIAPOficina.Application.Services.Commands.GetSingleService;
using FIAPOficina.Application.Services.Commands.UpdateService;
using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Services.Repositories;

namespace FIAPOficina.Application.Services.Services
{
    public class ServicesService : IServicesService
    {
        private readonly CreateServiceCommandHandler _createHandler;
        private readonly UpdateServiceCommandHandler _updateHandler;
        private readonly DeleteServiceCommandHandler _deleteHandler;
        private readonly GetSingleServiceCommandHandler _querySingleHandler;
        private readonly GetAllServicesCommandHandler _queryAllHandler;

        public ServicesService(IServiceRepository repository, IPasswordHasherService passwordHasher)
        {
            _createHandler = new CreateServiceCommandHandler(repository);
            _updateHandler = new UpdateServiceCommandHandler(repository);
            _deleteHandler = new DeleteServiceCommandHandler(repository);
            _querySingleHandler = new GetSingleServiceCommandHandler(repository);
            _queryAllHandler = new GetAllServicesCommandHandler(repository);
        }

        public async Task<Service> AddAsync(CreateServiceCommand command)
        {
            return await _createHandler.Handle(command);
        }

        public async Task<Service> UpdateAsync(UpdateServiceCommand command)
        {
            return await _updateHandler.Handle(command);
        }

        public async Task DeleteAsync(DeleteServiceCommand command)
        {
            await _deleteHandler.Handle(command);
        }

        public async Task<Service?> GetSingleAsync(GetSingleServiceCommand command)
        {
            return await _querySingleHandler.Handle(command).ConfigureAwait(false);
        }

        public Service[] GetAll(GetAllServicesCommand command)
        {
            return _queryAllHandler.Handle(command);
        }
    }
}