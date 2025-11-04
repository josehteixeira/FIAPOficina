using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Application.Common.Mail;
using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;
using FIAPOficina.Domain.Services.Entities;
using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Application.ServiceOrders.Commands.RequestServiceOrderApproval
{
    internal class RequestServiceOrderApprovalCommandHandler
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IServicesService _servicesService;
        private readonly IMaterialsService _materialsService;
        private readonly IVehiclesService _vehiclesService;
        private readonly IClientsService _clientsService;
        private readonly IMailService _mailService;

        public RequestServiceOrderApprovalCommandHandler(
            IServiceOrderRepository repository,
            IMailService mailService,
            IVehiclesService vehiclesService,
            IClientsService clientsService,
            IServicesService servicesService,
            IMaterialsService materialsService)
        {
            _repository = repository;
            _mailService = mailService;
            _vehiclesService = vehiclesService;
            _clientsService = clientsService;
            _servicesService = servicesService;
            _materialsService = materialsService;
        }

        public async Task Handle(RequestServiceOrderApprovalCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            if (serviceOrder.Status != ServiceOrderStatus.InDiagnosis)
            {
                throw new Exception("Only service orders with status \"In diagnosis\" can request approval!");
            }

            if (!serviceOrder.Materials.Any() && !serviceOrder.Services.Any())
            {
                throw new Exception("There must be at least one service or material linked to this service order!");
            }

            serviceOrder.Status = ServiceOrderStatus.WaitingApproval;
            await _repository.UpdateAsync(serviceOrder).ConfigureAwait(false);

            await SendMail(serviceOrder);
        }

        private async Task SendMail(ServiceOrder serviceOrder)
        {
            Vehicle vehicle = await GetVehicle(serviceOrder.VehicleId);
            Client client = await GetClient(vehicle.ClientId);

            string mailHtmlBody = CreateMailHtmlBody(serviceOrder, client, vehicle);

            _mailService.SendMail("", "Orçamento do seu veiculo", client.Email, mailHtmlBody);
        }

        private async Task<Vehicle> GetVehicle(Guid vehicleId)
        {
            var vehicle = await _vehiclesService.GetSingleAsync(new(vehicleId));

            if (vehicle is null)
                throw new Exception("Vehicle not found!");

            return vehicle;
        }

        private async Task<Client> GetClient(Guid clientId)
        {
            var client = await _clientsService.GetSingleAsync(new(clientId));

            if (client is null)
                throw new Exception("Client not found!");

            return client;
        }

        private string CreateMailHtmlBody(ServiceOrder serviceOrder, Client client, Vehicle vehicle)
        {
            var services = _servicesService
                .GetAll(new(serviceOrder.Services.Select(s => s.ServiceId).ToArray()))
                .ToDictionary(s => s.Id, s => s);

            var materials = _materialsService
                .GetAll(new(serviceOrder.Materials.Select(s => s.MaterialId).ToArray()))
                .ToDictionary(m => m.Id, m => m);

            string servicesRows = GetServicesRows(serviceOrder, services);
            var servicesSubtotal = serviceOrder.Services.Sum(s => s.TotalValue);
            string materialsRows = GetMaterialsRows(serviceOrder, materials);
            var materialsSubtotal = serviceOrder.Materials.Sum(s => s.TotalValue);
            var total = materialsSubtotal + servicesSubtotal;

            string mailHtml = LoadTemplate("ServiceOrderMail.html")
                .Replace("{client.name}", client.Name)
                .Replace("{services_rows}", servicesRows)
                .Replace("{services_subtotal}", servicesSubtotal.ToString("#.00"))
                .Replace("{materials_rows}", materialsRows)
                .Replace("{materials_subtotal}", materialsSubtotal.ToString("#.00"))
                .Replace("{total}", total.ToString("#.00"));

            return mailHtml;
        }

        private string GetServicesRows(ServiceOrder serviceOrder, Dictionary<Guid, Service> services)
        {
            return string.Join("", serviceOrder
                            .Services
                            .Select(s => $"<tr><td>{services[s.ServiceId].Name}</td><td>{s.Quantity}</td><td>R$ {s.UnitValue.ToString("#.00")}</td></tr>"));
        }

        private string GetMaterialsRows(ServiceOrder serviceOrder, Dictionary<Guid, Material> materials)
        {
            return string.Join("", serviceOrder
                            .Materials
                            .Select(m => $"<tr><td>{materials[m.MaterialId].Name}</td><td>{m.Quantity}</td><td>R$ {m.UnitValue.ToString("#.00")}</td></tr>"));
        }

        private async Task<ServiceOrder> GetServiceOrder(Guid id)
        {
            var oldServiceOrder = await _repository.FirstOrDefaultAsync(id).ConfigureAwait(false);

            if (oldServiceOrder is null)
            {
                throw new Exception("Service order not found!");
            }

            return oldServiceOrder;
        }

        public static string LoadTemplate(string templateName)
        {
            var basePath = AppContext.BaseDirectory;
            var templatePath = Path.Combine(basePath, "ServiceOrders", "Commands", "RequestServiceOrderApproval", $"{templateName}");

            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"Template '{templateName}' not found in '{templatePath}'!");

            return File.ReadAllText(templatePath);
        }
    }
}