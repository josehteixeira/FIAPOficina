namespace FIAPOficina.Application.ServiceOrders.Commands.UpdateServiceOrder
{
    public class ServiceOrderMaterialToUpdate
    {
        public Guid? Id { get; private set; }
        public Guid MaterialId { get; private set; }
        public int Quantity { get; private set; }

        public ServiceOrderMaterialToUpdate(Guid? id, Guid materialId, int quantity)
        {
            Id = id;
            MaterialId = materialId;
            Quantity = quantity;
        }
    }
}