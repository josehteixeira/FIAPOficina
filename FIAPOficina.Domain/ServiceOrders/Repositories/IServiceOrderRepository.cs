using FIAPOficina.Domain.ServiceOrders.Entities;

namespace FIAPOficina.Domain.ServiceOrders.Repositories
{
    public interface IServiceOrderRepository
    {
        Task<ServiceOrder> AddAsync(ServiceOrder serviceOrder);
        Task UpdateAsync(ServiceOrder serviceOrder);
        Task DeleteAsync(Guid id);
        Task<ServiceOrder?> FirstOrDefaultAsync(Guid id);
        Task<ServiceOrder?> FirstOrDefaultAsync(string plate);
        ServiceOrder[] GetAll(Guid? vehicle, Guid? clientId);
        ServiceOrder[] GetServicesOrderByVehiclePlate(string vehiclePlate);
    }
}
