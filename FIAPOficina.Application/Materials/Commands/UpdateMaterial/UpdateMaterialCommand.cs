namespace FIAPOficina.Application.Materials.Commands.UpdateMaterial
{
    public record UpdateMaterialCommand(Guid Id, string Name, string Description, string Brand, decimal Value, int Quantity);
}