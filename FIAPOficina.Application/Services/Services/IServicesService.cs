using FIAPOficina.Application.Services.Commands.CreateService;
using FIAPOficina.Application.Services.Commands.DeleteService;
using FIAPOficina.Application.Services.Commands.GetAllServices;
using FIAPOficina.Application.Services.Commands.GetSingleService;
using FIAPOficina.Application.Services.Commands.UpdateService;
using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Application.Services.Services
{
    public interface IServicesService
    {
        public Task<Service> AddAsync(CreateServiceCommand command);
        public Task<Service> UpdateAsync(UpdateServiceCommand command);
        public Task DeleteAsync(DeleteServiceCommand command);
        public Task<Service?> GetSingleAsync(GetSingleServiceCommand command);
        public Service[] GetAll(GetAllServicesCommand command);
    }
}