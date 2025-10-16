namespace FIAPOficina.Application.Vehicles.Commands.GetAllVehicles
{
    public class GetAllVehiclesCommand
    {
        public Guid? ClientId { get; private set; }

        public GetAllVehiclesCommand(Guid? clientID = null) { ClientId = clientID; }
    }
}