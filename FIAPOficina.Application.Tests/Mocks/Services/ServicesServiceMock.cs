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
        private List<Service> _services = new List<Service>();

        public Task<Service> AddAsync(CreateServiceCommand command)
        {
            Service service = new(command.Name, command.Description, command.Value);
            _services.Add(service);

            return Task.FromResult(service);
        }

        public Task DeleteAsync(DeleteServiceCommand command)
        {
            throw new NotImplementedException();
        }

        public Service[] GetAll(GetAllServicesCommand command)
        {
            _services.Add(new Service("", "", 1, Guid.Parse("B66A78BF-A800-4F18-B052-CEADF34558A7")));
            return _services.Where(m => command.Ids.Contains(m.Id)).ToArray();
        }

        public Task<Service?> GetSingleAsync(GetSingleServiceCommand command)
        {
            return Task.FromResult(_services.Where(m => command.Id == m.Id).FirstOrDefault());
        }

        public Task<Service> UpdateAsync(UpdateServiceCommand command)
        {
            Service service = _services.First(m => m.Id == command.Id);
            _services.Remove(service);

            service.Name = command.Name;
            service.Description = command.Description;
            service.Value = command.Value;

            _services.Add(service);

            return Task.FromResult(service);
        }
    }
}