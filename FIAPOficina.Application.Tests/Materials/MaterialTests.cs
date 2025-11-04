using FIAPOficina.Application.Materials.Commands.CreateMaterial;
using FIAPOficina.Application.Materials.Commands.DeleteMaterial;
using FIAPOficina.Application.Materials.Commands.GetAllMaterials;
using FIAPOficina.Application.Materials.Commands.GetSingleMaterial;
using FIAPOficina.Application.Materials.Commands.UpdateMaterial;
using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Application.Tests.Mocks.Repositories;
using FIAPOficina.Domain.Materials.Entities;

namespace FIAPOficina.Application.Tests.Materials
{
    public class MaterialTests
    {
        private readonly MaterialRepositoryMock _mock;
        public MaterialTests()
        {
            _mock = new MaterialRepositoryMock();
        }
        [Fact]
        public void Should_Create_Material_Valid()
        {
            MaterialsService materialsService = new MaterialsService(_mock);

            var material = materialsService.AddAsync(new CreateMaterialCommand("Pastilha de Freio", "Pastilha de freio", "Freia Mais", 20m, 4)).GetAwaiter().GetResult();

            Assert.NotNull(material);
            Assert.Equal("Pastilha de Freio", material.Name);
            Assert.Equal("Pastilha de freio", material.Description);
            Assert.Equal("Freia Mais", material.Brand);
            Assert.Equal(20.00m, material.Value);
            Assert.Equal(4, material.Quantity);

            var materialDb = materialsService.GetSingleAsync(new GetSingleMaterialCommand(material.Id)).GetAwaiter().GetResult();

            Assert.NotNull(materialDb);
            Assert.Equal("Pastilha de Freio", materialDb.Name);
            Assert.Equal("Pastilha de freio", materialDb.Description);
            Assert.Equal("Freia Mais", materialDb.Brand);
            Assert.Equal(20.00m, materialDb.Value);
            Assert.Equal(4, materialDb.Quantity);
        }
        
        [Fact]
        public void Should_Update_Material_Valid()
        {
            var mock = new MaterialRepositoryMock();

            MaterialsService materialsService = new MaterialsService(mock);
            var materialsDb = materialsService.GetAll(new GetAllMaterialsCommand());
            Material material;
            if (materialsDb is not null && materialsDb.Any())
                material = materialsDb[0];
            else
                material = materialsService.AddAsync(new CreateMaterialCommand("Pastilha de Freio", "Pastilha de freio", "Freia Mais", 20m, 4)).GetAwaiter().GetResult();

            var materialUpdated = materialsService.UpdateAsync(new UpdateMaterialCommand(material.Id, "Nova Pastilha de Freio", "Pastilha de freio melhor", "Freia Mais new", 25m, 40)).GetAwaiter().GetResult();

            Assert.NotNull(materialUpdated);
            Assert.Equal("Nova Pastilha de Freio", materialUpdated.Name);
            Assert.Equal("Pastilha de freio melhor", materialUpdated.Description);
            Assert.Equal("Freia Mais new", materialUpdated.Brand);
            Assert.Equal(25.00m, materialUpdated.Value);
            Assert.Equal(40, materialUpdated.Quantity);
        }

        [Fact]
        public void Should_Delete_All_Material()
        {
            var mock = new MaterialRepositoryMock();

            MaterialsService materialsService = new MaterialsService(mock);
            var material1 = materialsService.AddAsync(new CreateMaterialCommand("Pastilha de Freio", "Pastilha de freio", "Freia Mais", 20m, 4)).GetAwaiter().GetResult();
            var materialsDb = materialsService.GetAll(new GetAllMaterialsCommand([material1.Id]));

            foreach (var material in materialsDb)
                materialsService.DeleteAsync(new DeleteMaterialCommand(material.Id)).GetAwaiter();

            materialsDb = null;
            materialsDb = materialsService.GetAll(new GetAllMaterialsCommand());

            Assert.Empty(materialsDb);
        }
    }
}
