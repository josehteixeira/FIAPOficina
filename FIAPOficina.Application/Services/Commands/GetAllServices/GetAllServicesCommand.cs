namespace FIAPOficina.Application.Services.Commands.GetAllServices
{
    public class GetAllServicesCommand
    {
        public Guid[] Ids { get; set; }

        public GetAllServicesCommand() { }

        public GetAllServicesCommand(Guid[] ids)
        {
            Ids = ids;
        }
    }
}