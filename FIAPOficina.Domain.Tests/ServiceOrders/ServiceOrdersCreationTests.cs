using FIAPOficina.Domain.ServiceOrders.Entities;

namespace FIAPOficina.Domain.Tests.ServiceOrders
{
    public class ServiceOrdersCreationTests
    {
        [Fact]
        public void Should_Create_Valid_ServiceOrder_With_Vheicle()
        {
            Guid id = Guid.NewGuid();
            Guid vehicleId = Guid.NewGuid();

            ServiceOrder serviceOrder = new ServiceOrder(vehicleId, id);
            Assert.NotNull(serviceOrder);
            Assert.NotNull(serviceOrder.Materials);
            Assert.NotNull(serviceOrder.Services);
            Assert.Equal(ServiceOrderStatus.Received, serviceOrder.Status);
            Assert.Equal(id, serviceOrder.Id);
            Assert.Equal(vehicleId, serviceOrder.VehicleId);
        }
        [Fact]
        public void Should_Create_Valid_ServiceOrder_Using_Other_SerciceOrder()
        {
            Guid id = Guid.NewGuid();
            Guid vehicleId = Guid.NewGuid();

            ServiceOrder serviceOrderBase = new ServiceOrder(vehicleId, id);

            ServiceOrder serviceOrder = new ServiceOrder(serviceOrderBase, id);
            Assert.NotNull(serviceOrder);
            Assert.NotNull(serviceOrder.Materials);
            Assert.NotNull(serviceOrder.Services);
            Assert.Equal(ServiceOrderStatus.Received, serviceOrder.Status);
            Assert.Equal(id, serviceOrder.Id);
            Assert.Equal(vehicleId, serviceOrder.VehicleId);
        }

        [Fact]
        public void Should_Trhow_NullReference_Exeption()
        {
            Assert.Throws<NullReferenceException>(() => new ServiceOrder(null, Guid.NewGuid()));
        }


    }
}
