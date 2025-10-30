namespace FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder
{
    public class ServiceOrderServiceToCreate
    {
        public ServiceOrderServiceToCreate(Guid serviceId, int quantity)
        {
            ServiceId = serviceId;
            Quantity = quantity;
        }

        public Guid ServiceId { get; private set; }
        public int Quantity { get; private set; }
    }
}