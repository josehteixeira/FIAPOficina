namespace FIAPOficina.Application.Clients.Commands.CreateClient
{
    public record CreateClientCommand(string Name, string Identifier, string Phone, string Email, string Address);
}