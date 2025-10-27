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

        [Fact]
        public void Should_Create_invalid_ServiceOrderMaterial_Throw_ArgumentException_Quantity()
        {
            Guid materialId = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();
            Assert.Throws<ArgumentOutOfRangeException>(()=>new ServiceOrderMaterial(materialId, serviceOrderID, -1, 36.00m));
        }
        [Fact]
        public void Should_Create_Valid_ServiceOrderMaterial_Throw_ArgumentException_UnitValue()
        {
            Guid materialId = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();
            Assert.Throws<ArgumentOutOfRangeException>(() => new ServiceOrderMaterial(materialId, serviceOrderID, 4, -36.00m));
        }
        [Fact]
        public void Should_Create_Valid_ServiceOrderMaterial_From_Other_ServiceOrderMaterial()
        {
            Guid materialIdOriginal = Guid.NewGuid();
            Guid materialId = Guid.NewGuid();
            Guid serviceOrderIdOriginal = Guid.NewGuid();
            Guid serviceOrderID = Guid.NewGuid();

            ServiceOrderMaterial serviceOrderMaterialOriginal = new ServiceOrderMaterial(materialIdOriginal, serviceOrderIdOriginal, 4, 36.00m);
            ServiceOrderMaterial serviceOrderMaterial = new ServiceOrderMaterial(serviceOrderMaterialOriginal,materialId, serviceOrderID );
            Assert.NotNull(serviceOrderMaterial);
            Assert.Equal(serviceOrderMaterialOriginal.Quantity, serviceOrderMaterial.Quantity);
            Assert.Equal(serviceOrderMaterialOriginal.UnitValue, serviceOrderMaterial.UnitValue);
            Assert.Equal(serviceOrderMaterialOriginal.TotalValue, serviceOrderMaterial.TotalValue);
            Assert.Equal(materialId, serviceOrderMaterial.MaterialId);
            Assert.Equal(serviceOrderID, serviceOrderMaterial.ServiceOrderId);
        }
    }
}
