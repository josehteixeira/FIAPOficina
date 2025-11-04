using FIAPOficina.Application.ServiceOrders.Commands.ApproveServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.CompleteServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.DeleteServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.DeliverServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.GetAllServiceOrders;
using FIAPOficina.Application.ServiceOrders.Commands.GetAverageTime;
using FIAPOficina.Application.ServiceOrders.Commands.GetServicesOrderByVehicle;
using FIAPOficina.Application.ServiceOrders.Commands.GetSingleServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.RejectServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.RequestServiceOrderApproval;
using FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrder;
using FIAPOficina.Application.ServiceOrders.Commands.StartServiceOrderDiagnosis;
using FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder;
using FIAPOficina.Domain.ServiceOrders.Entities;

namespace FIAPOficina.Application.ServiceOrders.Services
{
    public interface IServiceOrderService
    {
        Task<ServiceOrder> AddAsync(CreateServiceOrderCommand command);
        Task<ServiceOrder> UpdateAsync(UpdateServiceOrderCommand command);
        Task DeleteAsync(DeleteServiceOrderCommand command);
        Task<ServiceOrder?> GetSingleAsync(GetSingleServiceOrderCommand command);
        ServiceOrder[] GetAll(GetAllServiceOrdersCommand command);
        Task ApproveServiceOrder(ApproveServiceOrderCommand command);
        Task CompleteServiceOrder(CompleteServiceOrderCommand command);
        Task DeliverServiceOrder(DeliverServiceOrderCommand command);
        Task RejectServiceOrder(RejectServiceOrderCommand command);
        Task RequestServiceOrderApproval(RequestServiceOrderApprovalCommand command);
        Task StartServiceOrder(StartServiceOrderCommand command);
        Task StartServiceOrderDiagnosis(StartServiceOrderDiagnosisCommand command);
        Task<ServiceOrder[]> GetServicesOrderByVehicle(GetServicesOrderByVehicleCommand command);
        TimeSpan? GetAverageTime(GetAverageTimeCommand command);
    }
}