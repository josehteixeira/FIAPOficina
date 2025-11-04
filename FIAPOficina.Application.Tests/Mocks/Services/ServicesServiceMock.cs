using FIAPOficina.Application.Services.Commands.CreateService;
using FIAPOficina.Application.Services.Commands.DeleteService;
using FIAPOficina.Application.Services.Commands.GetAllServices;
using FIAPOficina.Application.Services.Commands.GetSingleService;
using FIAPOficina.Application.Services.Commands.UpdateService;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Application.Tests.Mocks.Services
{
    internal class ServicesServiceMock : IServicesService
    {
        public Task<Service> AddAsync(CreateServiceCommand command)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DeleteServiceCommand command)
        {
            throw new NotImplementedException();
        }

        public Service[] GetAll(GetAllServicesCommand command)
        {
            return [new Service("","",1,Guid.Parse("B66A78BF-A800-4F18-B052-CEADF34558A7"))];
        }

        public Task<Service?> GetSingleAsync(GetSingleServiceCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<Service> UpdateAsync(UpdateServiceCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
