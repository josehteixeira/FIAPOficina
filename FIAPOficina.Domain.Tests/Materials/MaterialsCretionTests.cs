using FIAPOficina.Domain.Materials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Domain.Tests.Materials
{
    public class MaterialsCretionTests
    {
        [Fact]
        public void Should_Create_Valid_Material()
        {
            Guid id = Guid.NewGuid();

            Material material = new Material("Pastilha de Freio","Pastilha de freio muito resistente","Freia Bem",36.00m, id,4);
            Assert.NotNull(material);
            Assert.Equal("Pastilha de Freio", material.Name);
            Assert.Equal("Pastilha de freio muito resistente", material.Description);
            Assert.Equal("Freia Bem",material.Brand);
            Assert.Equal(36.00m, material.Value);
            Assert.Equal(4, material.Quantity);
            Assert.Equal(id, material.Id);
        }
    }
}
