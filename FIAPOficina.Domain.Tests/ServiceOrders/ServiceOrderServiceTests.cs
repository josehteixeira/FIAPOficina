using FIAPOficina.Domain.ServiceOrders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.Tests.ServiceOrders
{
    public class ServiceOrderServiceTests
    {
        [Fact]
        public void Should_Create_Valid_ServiceOrderService()
        {
            Guid serviceId = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();

            ServiceOrderService serviceOrderService = new ServiceOrderService(serviceId, serviceOrderID, 4, 27.00m);
            Assert.NotNull(serviceOrderService);
            Assert.Equal(4, serviceOrderService.Quantity);
            Assert.Equal(27.00m, serviceOrderService.UnitValue);
            Assert.Equal(108.00m, serviceOrderService.TotalValue);
            Assert.Equal(serviceId, serviceOrderService.ServiceId);
            Assert.Equal(serviceOrderID, serviceOrderService.ServiceOrderId);
        }

        [Fact]
        public void Should_Create_invalid_ServiceOrderService_Throw_ArgumentException_Quantity()
        {
            Guid serviceId = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();
            Assert.Throws<ArgumentOutOfRangeException>(() => new ServiceOrderService(serviceId, serviceOrderID, -1, 36.00m));
        }
        [Fact]
        public void Should_Create_Valid_ServiceOrderService_Throw_ArgumentException_UnitValue()
        {
            Guid materialId = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();
            Assert.Throws<ArgumentOutOfRangeException>(() => new ServiceOrderService(materialId, serviceOrderID, 4, -36.00m));
        }
        [Fact]
        public void Should_Create_Valid_ServiceOrderService_From_Other_ServiceOrderService()
        {
            Guid serviceIdOriginal = Guid.NewGuid();
            Guid serviceId = Guid.NewGuid();
            Guid serviceOrderIdOriginal = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();

            ServiceOrderService serviceOrderServiceOriginal = new ServiceOrderService(serviceIdOriginal, serviceOrderIdOriginal, 4, 36.00m);
            ServiceOrderService serviceOrderService = new ServiceOrderService(serviceOrderServiceOriginal, serviceId, serviceOrderID);
            Assert.NotNull(serviceOrderService);
            Assert.Equal(serviceOrderServiceOriginal.Quantity, serviceOrderService.Quantity);
            Assert.Equal(serviceOrderServiceOriginal.UnitValue, serviceOrderService.UnitValue);
            Assert.Equal(serviceOrderServiceOriginal.TotalValue, serviceOrderService.TotalValue);
            Assert.Equal(serviceId, serviceOrderService.ServiceId);
            Assert.Equal(serviceOrderID, serviceOrderService.ServiceOrderId);
        }
    }
}
