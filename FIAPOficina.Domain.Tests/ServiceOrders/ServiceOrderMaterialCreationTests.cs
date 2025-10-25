using FIAPOficina.Domain.ServiceOrders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.Tests.ServiceOrders
{
    public class ServiceOrderMaterialCreationTests
    {
        [Fact]
        public void Should_Create_Valid_ServiceOrderMaterial()
        {
            Guid materialId = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();

            ServiceOrderMaterial serviceOrderMaterial = new ServiceOrderMaterial(materialId, serviceOrderID,4,36.00m);
            Assert.NotNull(serviceOrderMaterial);
            Assert.Equal(4,serviceOrderMaterial.Quantity);
            Assert.Equal(36.00m,serviceOrderMaterial.UnitValue);
            Assert.Equal(144.00m, serviceOrderMaterial.TotalValue);
            Assert.Equal(materialId, serviceOrderMaterial.MaterialId);
            Assert.Equal(serviceOrderID, serviceOrderMaterial.ServiceOrderId);
        }
    }
}
