namespace FIAPOficina.Application.Materials.Commands.CreateMaterial
{
    public record CreateMaterialCommand(string Name, string Description, string Brand, decimal Value, int Quantity);
}