using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.Tests.Mocks.Repositories
{
    internal class ServiceOrderRepositoryMock : IServiceOrderRepository
    {
        private List<ServiceOrder> _serviceOrders = new List<ServiceOrder>();
        public async Task<ServiceOrder> AddAsync(ServiceOrder serviceOrder)
        {
            serviceOrder.Id = Guid.NewGuid();
            _serviceOrders.Add(serviceOrder);
            return serviceOrder;

        }

        public async Task DeleteAsync(Guid id)
        {
            var osDb = FirstOrDefaultAsync(id).GetAwaiter().GetResult();
            _serviceOrders.Remove(osDb);

        }

        public async Task<ServiceOrder?> FirstOrDefaultAsync(Guid id)
        {
            return _serviceOrders.FirstOrDefault(x => x.Id == id);
        }

        public Task<ServiceOrder?> FirstOrDefaultAsync(string plate)
        {
            throw new NotImplementedException();
        }

        public ServiceOrder[] GetAll(Guid? vehicle, Guid? clientId)
        {
            return _serviceOrders.ToArray();
        }

        public async Task UpdateAsync(ServiceOrder serviceOrder)
        {
            var osDb = FirstOrDefaultAsync(serviceOrder.Id).GetAwaiter().GetResult();
            if (osDb is not null)
            {
                osDb.Services = serviceOrder.Services;
                osDb.Status = serviceOrder.Status;
                osDb.Materials = serviceOrder.Materials;
                osDb.VehicleId = serviceOrder.VehicleId;
            }
        }
    }
}
