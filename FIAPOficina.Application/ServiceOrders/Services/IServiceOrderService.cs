using FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.DeleteServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders;
using FIAPOficina.Application.ServiceOrders.Commands.GetSingleServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder;
using FIAPOficina.Domain.ServiceOrders.Entities;

namespace FIAPOficina.Application.ServiceOrders.Services
{
    public interface IServiceOrderService
    {
        public Task<ServiceOrder> AddAsync(CreateServiceOrderCommand command);
        public Task<ServiceOrder> UpdateAsync(UpdateServiceOrderCommand command);
        public Task DeleteAsync(DeleteServiceOrderCommand command);
        public Task<ServiceOrder?> GetSingleAsync(GetSingleServiceOrderCommand command);
        public ServiceOrder[] GetAll(GetAllServiceOrdersCommand command);
    }
}