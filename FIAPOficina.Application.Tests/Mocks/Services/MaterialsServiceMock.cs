using FIAPOficina.Application.Materials.Commands.CreateMaterial;
using FIAPOficina.Application.Materials.Commands.DeleteMaterial;
using FIAPOficina.Application.Materials.Commands.GetAllMaterials;
using FIAPOficina.Application.Materials.Commands.GetSingleMaterial;
using FIAPOficina.Application.Materials.Commands.UpdateMaterial;
using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Domain.Materials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Mocks.Services
{
    internal class MaterialsServiceMock : IMaterialsService
    {
        public Task<Material> AddAsync(CreateMaterialCommand command)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DeleteMaterialCommand command)
        {
            throw new NotImplementedException();
        }

        public Material[] GetAll(GetAllMaterialsCommand command)
        {
            return new Material[2];
        }

        public Task<Material?> GetSingleAsync(GetSingleMaterialCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<Material> UpdateAsync(UpdateMaterialCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
