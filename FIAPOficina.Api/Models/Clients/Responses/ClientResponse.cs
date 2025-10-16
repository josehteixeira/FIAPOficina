namespace FIAPOficina.Api.Models.Clients.Responses
{
    public record ClientResponse(
        Guid Id,
        string Name,
        string Identifier,
        string Phone,
        string Email,
        string Address
    );
}