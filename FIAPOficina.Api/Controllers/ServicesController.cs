using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.Services.Requests;
using FIAPOficina.Api.Models.Services.Responses;
using FIAPOficina.Application.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAPOficina.Api.Controllers
{
    [ApiController]
    [Route(RoutesHelper.Services.Controller)]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Create service.",
            Description = "Creates a service with the provided info."
        )]
        [HttpPost(RoutesHelper.Services.Create)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<ServiceResponse>> Create([FromBody] CreateServiceRequest request)
        {
            var service = await _servicesService.AddAsync(new(
                Name: request.Name,
                Description: request.Description,
                Value: request.Value
            ));

            return Created((Uri)null!, new ServiceResponse(
                Id: service.Id,
                Name: service.Name,
                Description: service.Description,
                Value: service.Value)
            );
        }


        [Authorize]
        [SwaggerOperation(
            Summary = "Update service.",
            Description = "Upates a service with the provided info."
        )]
        [HttpPut(RoutesHelper.Services.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateServiceRequest request, [FromRoute] Guid id)
        {
            var service = await _servicesService.UpdateAsync(new(
                Id: id,
                Name: request.Name,
                Description: request.Description,
                Value: request.Value
            ));

            return NoContent();
        }


        [Authorize]
        [SwaggerOperation(
            Summary = "Delete service.",
            Description = "Deletes the service with the provided ID."
        )]
        [HttpDelete(RoutesHelper.Services.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _servicesService.DeleteAsync(new(id));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get service.",
            Description = "Returns the service that matches the provided ID."
        )]
        [HttpGet(RoutesHelper.Services.GetSingle)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<ActionResult<ServiceResponse>> GetSingle([FromRoute] Guid id)
        {
            var service = await _servicesService.GetSingleAsync(new(id));

            if (service is null)
            {
                return NotFound();
            }

            return Ok(new ServiceResponse(
                    Id: service.Id,
                    Name: service.Name,
                    Description: service.Description,
                    Value: service.Value));
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get services.",
            Description = "Returns all services."
        )]
        [HttpGet(RoutesHelper.Services.GetAll)]
        [ProducesResponseType(typeof(ServiceResponse[]), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public ActionResult<ServiceResponse[]> GetAll()
        {
            var services = _servicesService.GetAll(new());

            if (services is not null && services.Length > 0)
            {
                return Ok(services.Select(service => new ServiceResponse(
                    Id: service.Id,
                    Name: service.Name,
                    Description: service.Description,
                    Value: service.Value)
                ).ToArray());
            }

            return Ok(Array.Empty<ServiceResponse>());
        }
    }
}