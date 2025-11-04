using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.GetAverageTime
{
    internal class GetAverageTimeCommandHandler
    {
        private readonly IServiceOrderRepository _repository;

        public GetAverageTimeCommandHandler(IServiceOrderRepository repository)
        {
            _repository = repository;
        }

        public TimeSpan? Handle(GetAverageTimeCommand command)
        {
            var serviceOrders = _repository.GetAll(null, null);

            TimeSpan timeSpan = new TimeSpan();
            int count = 0;

            foreach (var serviceOrder in serviceOrders)
            {
                if (serviceOrder.FinishedOn.HasValue && serviceOrder.ApprovedOn.HasValue)
                {
                    timeSpan += serviceOrder.FinishedOn.Value - serviceOrder.ApprovedOn.Value;
                    count++;
                }
            }

            if (count == 0) return null;

            return timeSpan / count;
        }
    }
}