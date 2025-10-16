namespace FIAPOficina.Api.Models.Services.Responses
{
    public record ServiceResponse
    (
        Guid Id,
        string Name,
        string Description,
        double Value
    );
}
