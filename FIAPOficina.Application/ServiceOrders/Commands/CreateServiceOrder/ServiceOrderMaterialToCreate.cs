namespace FIAPOficina.Application.ServiceOrders.Commands.CreateServiceOrder
{
    public class ServiceOrderMaterialToCreate
    {
        public Guid MaterialId { get; private set; }
        public int Quantity { get; private set; }

        public ServiceOrderMaterialToCreate(Guid materialId, int quantity)
        {
            MaterialId = materialId;
            Quantity = quantity;
        }
    }
}