using FIAPOficina.Domain.Materials.Entities;

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
    }
}
