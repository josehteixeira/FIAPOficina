namespace FIAPOficina.Application.Clients.Commands.UpdateClient
{
    public record UpdateClientCommand(Guid Id, string Name, string Identifier, string Phone, string Email, string Address);
}