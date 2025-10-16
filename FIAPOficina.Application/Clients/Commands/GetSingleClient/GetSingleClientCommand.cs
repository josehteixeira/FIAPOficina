namespace FIAPOficina.Application.Clients.Commands.GetSingleClient
{
    public class GetSingleClientCommand
    {
        public Guid Id { get; private set; }
        public string? Identifier { get; private set; }

        public GetSingleClientCommand(Guid id) { Id = id; }
        public GetSingleClientCommand(string identifier) { Identifier = identifier; }
    }
}