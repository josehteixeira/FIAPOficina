using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Domain.ServiceOrders.Repositories
{
    public interface IServiceOrderRespository
    {
        Task<ServiceOrder> AddAsync(ServiceOrder serviceOrder);
        Task UpdateAsync(ServiceOrder serviceOrder);
        Task DeleteAsync(Guid id);
        Task<Vehicle?> FirstOrDefaultAsync(Guid id);
        Task<ServiceOrder?> FirstOrDefaultAsync(string plate);
        ServiceOrder[] GetAll(Guid? vehicle);
        Task AddMaterial(Guid serviceOrderId,Material material, int quantity);
        Task UpdateMaterial(Guid serviceOrderId, Guid materialId, int quantity);
        Task RemoveMaterial(Guid serviceOrderId,Guid materialId);
        Task AddService( Guid serviceOrderId, Service service, int quantity);
        Task UpdateService(Guid serviceOrderId, Guid serviceId, int quantity);
        Task DeleteService(Guid serviceOrderId, Guid serviceId);
    }
}
