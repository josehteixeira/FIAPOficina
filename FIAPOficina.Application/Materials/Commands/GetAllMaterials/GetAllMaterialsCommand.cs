namespace FIAPOficina.Application.Materials.Commands.GetAllMaterials
{
    public class GetAllMaterialsCommand
    {
        public Guid[] Ids { get; set; } = Array.Empty<Guid>();

        public GetAllMaterialsCommand() { }

        public GetAllMaterialsCommand(Guid[] ids)
        {
            Ids = ids;
        }
    }
}