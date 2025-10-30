namespace FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder
{
    public class ServiceOrderServiceToUpdate
    {
        public Guid? Id { get; private set; }
        public Guid ServiceId { get; private set; }
        public int Quantity { get; private set; }

        public ServiceOrderServiceToUpdate(Guid? id, Guid serviceId, int quantity)
        {
            Id = id;
            ServiceId = serviceId;
            Quantity = quantity;
        }
    }
}