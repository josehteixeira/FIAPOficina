using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.DeleteServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders;
using FIAPOficina.Application.ServiceOrders.Commands.GetSingleServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Services
{
    public class ServiceOrderService : IServiceOrderService
    {
        private readonly CreateServiceOrderCommandHandler _createHandler;
        private readonly UpdateServiceOrderCommandHandler _updateHandler;
        private readonly DeleteServiceOrderCommandHandler _deleteHandler;
        private readonly GetSingleServiceOrderCommandHandler _querySingleHandler;
        private readonly GetAllServiceOrdersCommandHandler _queryAllHandler;

        public ServiceOrderService(
            IServiceOrderRepository repository,
            IVehiclesService vehiclesService,
            IMaterialsService materialsService,
            IServicesService servicesService)
        {
            _createHandler = new CreateServiceOrderCommandHandler(repository, vehiclesService, materialsService, servicesService);
            _updateHandler = new UpdateServiceOrderCommandHandler(repository, materialsService, servicesService);
            _deleteHandler = new DeleteServiceOrderCommandHandler(repository);
            _querySingleHandler = new GetSingleServiceOrderCommandHandler(repository);
            _queryAllHandler = new GetAllServiceOrdersCommandHandler(repository);
        }

        public async Task<Domain.ServiceOrders.Entities.ServiceOrder> AddAsync(CreateServiceOrderCommand command)
        {
            return await _createHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task DeleteAsync(DeleteServiceOrderCommand command)
        {
            await _deleteHandler.Handle(command).ConfigureAwait(false);
        }

        public Domain.ServiceOrders.Entities.ServiceOrder[] GetAll(GetAllServiceOrdersCommand command)
        {
            return _queryAllHandler.Handle(command);
        }

        public async Task<Domain.ServiceOrders.Entities.ServiceOrder?> GetSingleAsync(GetSingleServiceOrderCommand command)
        {
            return await _querySingleHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task<Domain.ServiceOrders.Entities.ServiceOrder> UpdateAsync(UpdateServiceOrderCommand command)
        {
            return await _updateHandler.Handle(command).ConfigureAwait(false);
        }
    }
}