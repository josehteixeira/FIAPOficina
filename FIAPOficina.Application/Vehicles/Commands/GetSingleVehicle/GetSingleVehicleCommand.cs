namespace FIAPOficina.Application.Vehicles.Commands.GetSingleVehicle
{
    public class GetSingleVehicleCommand
    {
        public Guid Id { get; private set; }
        public string? Plate { get; private set; }

        public GetSingleVehicleCommand(Guid id) { Id = id; }

        public GetSingleVehicleCommand(string plate) { Plate = plate; }
    }
}