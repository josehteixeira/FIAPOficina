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
        [Fact]
        public void Should_Create_Valid_Material_By_Existent_Material()
        {
            Guid id = Guid.NewGuid();

            Material material = new Material("Pastilha de Freio","Pastilha de freio muito resistente","Freia Bem",36.00m, 4, id);

            var newMaterial=new Material(material, Guid.NewGuid());
            Assert.NotNull(newMaterial);
            Assert.Equal( material.Name, newMaterial.Name);
            Assert.Equal( material.Description, newMaterial.Description);
            Assert.Equal( material.Brand, newMaterial.Brand);
            Assert.Equal(material.Value, newMaterial.Value);
            Assert.Equal(material.Quantity, newMaterial.Quantity);
            Assert.NotEqual(id, newMaterial.Id);
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
