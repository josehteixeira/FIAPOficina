using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.Vehicles.Requests;
using FIAPOficina.Api.Models.Vehicles.Responses;
using FIAPOficina.Application.Vehicles.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAPOficina.Api.Controllers
{
    [ApiController]
    [Route(RoutesHelper.Vehicles.Controller)]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesService _vehiclesService;

        public VehiclesController(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Create vehicle.",
            Description = "Creates a vehicle with the provided info."
        )]
        [HttpPost(RoutesHelper.Vehicles.Create)]
        [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<VehicleResponse>> Create([FromBody] CreateVehicleRequest request)
        {
            var vehicle = await _vehiclesService.AddAsync(new(
                Brand: request.Brand,
                Model: request.Model,
                Year: request.Year,
                Plate: request.Plate,
                Color: request.Color,
                ClientId: request.ClientId
            ));

            return Created((Uri)null!, new VehicleResponse(
                Id: vehicle.Id,
                Brand: vehicle.Brand,
                Model: vehicle.Model,
                Year: vehicle.Year,
                Plate: vehicle.Plate,
                Color: vehicle.Color,
                ClientId: vehicle.ClientId
            ));
        }


        [Authorize]
        [SwaggerOperation(
            Summary = "Update vehicle.",
            Description = "Upates a vehicle with the provided info."
        )]
        [HttpPut(RoutesHelper.Vehicles.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateVehicleRequest request, [FromRoute] Guid id)
        {
            var vehicle = await _vehiclesService.UpdateAsync(new(
                Id: id,
                Brand: request.Brand,
                Model: request.Model,
                Year: request.Year,
                Plate: request.Plate,
                Color: request.Color
            ));

            return NoContent();
        }


        [Authorize]
        [SwaggerOperation(
            Summary = "Delete vehicle.",
            Description = "Deletes the vehicle with the provided ID."
        )]
        [HttpDelete(RoutesHelper.Vehicles.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _vehiclesService.DeleteAsync(new(id));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get vehicle.",
            Description = "Returns the vehicle that matches the provided ID."
        )]
        [HttpGet(RoutesHelper.Vehicles.GetSingle)]
        [ProducesResponseType(typeof(VehicleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<ActionResult<VehicleResponse>> GetSingle([FromRoute] Guid id)
        {
            var vehicle = await _vehiclesService.GetSingleAsync(new(id));

            if (vehicle is null)
            {
                return NotFound();
            }

            return Ok(new VehicleResponse(
                    Id: vehicle.Id,
                    Brand: vehicle.Brand,
                    Model: vehicle.Model,
                    Year: vehicle.Year,
                    Plate: vehicle.Plate,
                    Color: vehicle.Color,
                    ClientId: vehicle.ClientId));
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get vehicles.",
            Description = "Returns all vehicles."
        )]
        [HttpGet(RoutesHelper.Vehicles.GetAll)]
        [ProducesResponseType(typeof(VehicleResponse[]), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public ActionResult<VehicleResponse[]> GetAll()
        {
            var vehicles = _vehiclesService.GetAll(new());

            if (vehicles is not null && vehicles.Length > 0)
            {
                return Ok(vehicles.Select(vehicle => new VehicleResponse(
                    Id: vehicle.Id,
                    Brand: vehicle.Brand,
                    Model: vehicle.Model,
                    Year: vehicle.Year,
                    Plate: vehicle.Plate,
                    Color: vehicle.Color,
                    ClientId: vehicle.ClientId)
                ).ToArray());
            }

            return Ok(Array.Empty<VehicleResponse>());
        }
    }
}