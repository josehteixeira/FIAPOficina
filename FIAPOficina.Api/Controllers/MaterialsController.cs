using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.Materials.Requests;
using FIAPOficina.Api.Models.Materials.Responses;
using FIAPOficina.Application.Materials.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAPOficina.Api.Controllers
{
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialsService _materialsService;

        public MaterialsController(IMaterialsService materialsService)
        {
            _materialsService = materialsService;
        }

        [Authorize]
        [HttpPost(RoutesHelper.Materials.Create)]
        [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<MaterialResponse>> Create([FromBody] CreateMaterialRequest request)
        {
            var material = await _materialsService.AddAsync(new(
                Name: request.Name,
                Description: request.Description,
                Brand: request.Brand,
                Value: request.Value,
                Quantity: request.Quantity
            ));

            return Created((Uri)null!, new MaterialResponse(
                Id: material.Id,
                Name: material.Name,
                Description: material.Description,
                Brand: material.Brand,
                Value: material.Value,
                Quantity: material.Quantity)
            );
        }


        [Authorize]
        [HttpPut(RoutesHelper.Materials.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateMaterialRequest request, [FromRoute] Guid id)
        {
            var material = await _materialsService.UpdateAsync(new(
                Id: id,
                Name: request.Name,
                Description: request.Description,
                Brand: request.Brand,
                Value: request.Value,
                Quantity: request.Quantity
            ));

            return NoContent();
        }


        [Authorize]
        [HttpDelete(RoutesHelper.Materials.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _materialsService.DeleteAsync(new(id));

            return Ok();
        }

        [Authorize]
        [HttpGet(RoutesHelper.Materials.GetSingle)]
        [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<MaterialResponse>> GetSingle([FromRoute] Guid id)
        {
            var material = await _materialsService.GetSingleAsync(new(id));

            return Ok(material);
        }

        [Authorize]
        [HttpGet(RoutesHelper.Materials.GetAll)]
        [ProducesResponseType(typeof(MaterialResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public ActionResult<MaterialResponse[]> GetAll()
        {
            var materials = _materialsService.GetAll(new());

            if (materials is not null && materials.Length > 0)
            {
                return Ok(materials.Select(material => new MaterialResponse(
                    Id: material.Id,
                    Name: material.Name,
                    Description: material.Description,
                    Brand: material.Brand,
                    Value: material.Value,
                    Quantity: material.Quantity)
                ).ToArray());
            }

            return Ok(Array.Empty<MaterialResponse>());
        }
    }
}