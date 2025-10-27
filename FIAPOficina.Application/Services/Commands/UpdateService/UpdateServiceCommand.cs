namespace FIAPOficina.Application.Services.Commands.UpdateService
{
    public record UpdateServiceCommand(Guid Id, string Name, string Description, decimal Value);
}