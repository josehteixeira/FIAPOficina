namespace FIAPOficina.Api.Models.Vehicles.Responses
{
    public record VehicleResponse
    (
        Guid Id,
        string Brand,
        string Model,
        int Year,
        string Plate,
        string Color,
        Guid ClientId
    );
}
