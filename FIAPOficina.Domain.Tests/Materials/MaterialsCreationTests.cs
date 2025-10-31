using FIAPOficina.Domain.Materials.Entities;
using FIAPOficina.Domain.Services.Entities;

namespace FIAPOficina.Domain.Tests.Materials
{
    public class MaterialsCreationTests
    {
        [Fact]
        public void Should_Create_Valid_Material()
        {
            Guid id = Guid.NewGuid();

            Material material = new Material("Pastilha de Freio","Pastilha de freio muito resistente","Freia Bem",36.00m, 4, id);
            Assert.NotNull(material);
            Assert.Equal("Pastilha de Freio", material.Name);
            Assert.Equal("Pastilha de freio muito resistente", material.Description);
            Assert.Equal("Freia Bem", material.Brand);
            Assert.Equal(36.00m, material.Value);
            Assert.Equal(4, material.Quantity);
            Assert.Equal(id, material.Id);
        }
        [Fact]
        public void Should_Throw_ArgumentException_By_UnitValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Material("Pastilha de Freio", "Pastilha de freio muito resistente", "Freia Bem", -36.00m, 4));
        }

        [Fact]
        public void Should_Throw_ArgumentException_By_Quantity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Material("Pastilha de Freio", "Pastilha de freio muito resistente", "Freia Bem", 36.00m, -4));
        }
    }
}
