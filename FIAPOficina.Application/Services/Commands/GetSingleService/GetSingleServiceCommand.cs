namespace FIAPOficina.Application.Services.Commands.GetSingleService
{

    public class GetSingleServiceCommand
    {
        public Guid Id { get; private set; }

        public GetSingleServiceCommand(Guid id) { Id = id; }
    }
}