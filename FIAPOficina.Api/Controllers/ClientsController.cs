using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.Clients.Requests;
using FIAPOficina.Api.Models.Clients.Responses;
using FIAPOficina.Application.Clients.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAPOficina.Api.Controllers
{
    [ApiController]
    [Route(RoutesHelper.Clients.Controller)]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Create client.",
            Description = "Creates a client with the provided info."
        )]
        [HttpPost(RoutesHelper.Clients.Create)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<ClientResponse>> Create([FromBody] CreateClientRequest request)
        {
            var client = await _clientsService.AddAsync(new(
                Name: request.Name,
                Identifier: request.Identifier,
                Phone: request.Phone,
                Email: request.Email,
                Address: request.Address
            ));

            return Created((Uri)null!, new ClientResponse(
                Id: client.Id,
                Name: client.Name,
                Identifier: client.Identifier,
                Phone: client.Phone,
                Email: client.Email,
                Address: client.Address)
            );
        }


        [Authorize]
        [SwaggerOperation(
            Summary = "Update client.",
            Description = "Upates a client with the provided info."
        )]
        [HttpPut(RoutesHelper.Clients.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateClientRequest request, [FromRoute] Guid id)
        {
            var client = await _clientsService.UpdateAsync(new(
                Id: id,
                Name: request.Name,
                Identifier: request.Identifier,
                Phone: request.Phone,
                Email: request.Email,
                Address: request.Address
            ));

            return NoContent();
        }


        [Authorize]
        [SwaggerOperation(
            Summary = "Delete client.",
            Description = "Deletes the client with the provided ID."
        )]
        [HttpDelete(RoutesHelper.Clients.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _clientsService.DeleteAsync(new(id));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get client.",
            Description = "Returns the client that matches the provided ID."
        )]
        [HttpGet(RoutesHelper.Clients.GetSingle)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<ActionResult<ClientResponse>> GetSingle([FromRoute] Guid id)
        {
            var client = await _clientsService.GetSingleAsync(new(id));

            if (client is null)
            {
                return NotFound();
            }

            return Ok(new ClientResponse(
                    Id: client.Id,
                    Name: client.Name,
                    Identifier: client.Identifier,
                    Phone: client.Phone,
                    Email: client.Email,
                    Address: client.Address));
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get clients.",
            Description = "Returns all clients."
        )]
        [HttpGet(RoutesHelper.Clients.GetAll)]
        [ProducesResponseType(typeof(ClientResponse[]), StatusCodes.Status200OK)]
        [Consumes("application/json")]
        public ActionResult<ClientResponse[]> GetAll()
        {
            var clients = _clientsService.GetAll(new());

            if (clients is not null && clients.Length > 0)
            {
                return Ok(clients.Select(client => new ClientResponse(
                    Id: client.Id,
                    Name: client.Name,
                    Identifier: client.Identifier,
                    Phone: client.Phone,
                    Email: client.Email,
                    Address: client.Address)
                ).ToArray());
            }

            return Ok(Array.Empty<ClientResponse>());
        }
    }
}