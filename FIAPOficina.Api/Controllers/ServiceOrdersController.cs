using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.ServiceOrders.Requests;
using FIAPOficina.Api.Models.ServiceOrders.Responses;
using FIAPOficina.Application.ServiceOrders.Services;
using FIAPOficina.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAPOficina.Api.Controllers
{
    public class ServiceOrdersController : ControllerBase
    {
        private readonly IServiceOrderService _serviceOrdersService;

        public ServiceOrdersController(IServiceOrderService materialsService)
        {
            _serviceOrdersService = materialsService;
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Create service order.",
            Description = "Creates a service order for the vehicle with the provided Materials and Services."
        )]
        [HttpPost(RoutesHelper.ServiceOrders.Create)]
        [ProducesResponseType(typeof(ServiceOrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<ServiceOrderResponse>> Create([FromBody] ServiceOrderRequest request)
        {
            var serviceOrder = await _serviceOrdersService.AddAsync(new(
                request.VehicleId,
                request.Services.Select(service => service.ToCreationModel()).ToList(),
                request.Materials.Select(material => material.ToCreationModel()).ToList()
            ));

            return Created((Uri)null!, new ServiceOrderResponse()
            {
                Id = serviceOrder.Id,
                Status = (int)serviceOrder.Status,
                VehicleId = serviceOrder.VehicleId,
                Materials = serviceOrder.Materials.Select(material => material.ToResponse()).ToArray(),
                Services = serviceOrder.Services.Select(service => service.ToResponse()).ToArray(),
            });
        }


        [Authorize]
        [SwaggerOperation(
            Summary = "Update service order.",
            Description = "Updates the service order that matches the provided ID. Materials and Services with no ID will be added to the service order,  items with ID will be updated."
        )]
        [HttpPut(RoutesHelper.ServiceOrders.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] ServiceOrderRequest request, [FromRoute] Guid id)
        {
            var material = await _serviceOrdersService.UpdateAsync(new(
                Id: id,
                VehicleId: request.VehicleId,
                Services: request.Services.Select(service => service.ToUpdateModel()).ToList(),
                Materials: request.Materials.Select(material => material.ToUpdateModel()).ToList()
            ));

            return NoContent();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Delete service order.",
            Description = "Deletes the service order that matches the provided ID."
        )]
        [HttpDelete(RoutesHelper.ServiceOrders.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _serviceOrdersService.DeleteAsync(new(id));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get service order.",
            Description = "Returns the service order that matches the provided ID with all of its information and status."
        )]
        [HttpGet(RoutesHelper.ServiceOrders.GetSingle)]
        [ProducesResponseType(typeof(ServiceOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<ServiceOrderResponse>> GetSingle([FromRoute] Guid id)
        {
            var material = await _serviceOrdersService.GetSingleAsync(new(id));

            return Ok(material);
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get all service orders.",
            Description = "Returns all of the service orders with all of their information and status."
        )]
        [HttpGet(RoutesHelper.ServiceOrders.GetAll)]
        [ProducesResponseType(typeof(ServiceOrderResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public ActionResult<ServiceOrderResponse[]> GetAll()
        {
            var orders = _serviceOrdersService.GetAll(new());

            if (orders is not null && orders.Length > 0)
            {
                return Ok(orders.Select(serviceOrder => new ServiceOrderResponse()
                {
                    Id = serviceOrder.Id,
                    VehicleId = serviceOrder.VehicleId,
                    Status = (int)serviceOrder.Status,
                    Materials = serviceOrder.Materials.Select(material => material.ToResponse()).ToArray(),
                    Services = serviceOrder.Services.Select(service => service.ToResponse()).ToArray(),
                    CreatedOn = serviceOrder.CreatedOn,
                    ApprovedOn = serviceOrder.ApprovedOn,
                    FinishedOn = serviceOrder.FinishedOn,
                }).ToArray());
            }

            return Ok(Array.Empty<ServiceOrderResponse>());
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Start diagnosis for this service order.",
            Description = "Sets the service order status as \"In diagnosis\", only possible if service order status is currently \"Received\"."
        )]
        [HttpPost(RoutesHelper.ServiceOrders.StartServiceOrderDiagnosis)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> StartServiceOrderDiagnosis([FromRoute] Guid id)
        {
            await _serviceOrdersService.StartServiceOrderDiagnosis(new(id));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Request approval for this service order.",
            Description = "Sets the service order status as \"Waiting approval\", only possible if service order status is currently \"InDiagnosis\"."
        )]
        [HttpPost(RoutesHelper.ServiceOrders.RequestServiceOrderApproval)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> RequestServiceOrderApproval([FromRoute] Guid id)
        {
            await _serviceOrdersService.RequestServiceOrderApproval(new(id));

            return Ok();
        }

        [SwaggerOperation(
            Summary = "Approve service order.",
            Description = "Sets the service order status as \"Approved\", only possible if service order status is currently \"Waiting approval\"."
        )]
        [HttpPost(RoutesHelper.ServiceOrders.ApproveServiceOrder)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> ApproveServiceOrder([FromRoute] Guid id, [FromBody] ApproveServiceOrderRequest request)
        {
            await _serviceOrdersService.ApproveServiceOrder(new(id, request.ClientIdentifier, request.VehiclePlate));

            return Ok();
        }

        [SwaggerOperation(
            Summary = "Reject service order.",
            Description = "Sets the service order status as \"Rejected\", only possible if service order status is currently \"Awaiting approval\"."
        )]
        [HttpPost(RoutesHelper.ServiceOrders.RejectServiceOrder)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> RejectServiceOrder([FromRoute] Guid id, [FromBody] ApproveServiceOrderRequest request)
        {
            await _serviceOrdersService.RejectServiceOrder(new(id, request.ClientIdentifier, request.VehiclePlate));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Start service order.",
            Description = "Sets the service order status as \"Running\", only possible if service order status is currently \"Approved\"."
        )]
        [Authorize]
        [HttpPost(RoutesHelper.ServiceOrders.StartServiceOrder)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> StartServiceOrder([FromRoute] Guid id)
        {
            await _serviceOrdersService.StartServiceOrder(new(id));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Complete service order",
            Description = "Sets the service order status as \"Completed\", only possible if service order status is currently \"Running\"."
        )]
        [HttpPost(RoutesHelper.ServiceOrders.CompleteServiceOrder)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> CompleteServiceOrder([FromRoute] Guid id)
        {
            await _serviceOrdersService.CompleteServiceOrder(new(id));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Sets service order as \"Delivered\".",
            Description = "Sets the service order status as \"Delivered\", only possible if service order status is currently \"Completed\"."
        )]
        [HttpPost(RoutesHelper.ServiceOrders.DeliverServiceOrder)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> DeliverServiceOrder([FromRoute] Guid id)
        {
            await _serviceOrdersService.DeliverServiceOrder(new(id));

            return Ok();
        }

        [SwaggerOperation(
            Summary = "Gets the service orders of the given vehicle.",
            Description = "Retrieves the service orders status of the provided vehicle, validating the client identifier. 204 if no service order is found."
        )]
        [HttpGet(RoutesHelper.ServiceOrders.GetClientVehicleServiceOrders)]
        [ProducesResponseType(typeof(ServiceOrderResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceOrderResponse[]), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<ServiceOrderResponse[]>> GetClientVehicleServiceOrders([FromRoute] string clientIdentifier, [FromRoute] string vehiclePlate)
        {
            var orders = await _serviceOrdersService.GetServicesOrderByVehicle(new(clientIdentifier, vehiclePlate));

            if (orders is not null && orders.Length > 0)
            {
                return Ok(orders.Select(serviceOrder => new ServiceOrderResponse()
                {
                    Id = serviceOrder.Id,
                    VehicleId = serviceOrder.VehicleId,
                    Status = (int)serviceOrder.Status,
                    Materials = serviceOrder.Materials.Select(material => material.ToResponse()).ToArray(),
                    Services = serviceOrder.Services.Select(service => service.ToResponse()).ToArray(),
                    CreatedOn = serviceOrder.CreatedOn,
                    ApprovedOn = serviceOrder.ApprovedOn,
                    FinishedOn = serviceOrder.FinishedOn,
                }).ToArray());
            }

            return Ok(Array.Empty<ServiceOrderResponse>());
        }

        [Authorize]
        [SwaggerOperation(
            Summary = "Get the average service order processing time.",
            Description = "Retrieves the average processing time of service orders that have been approved and finished. 204 if no service order is found."
        )]
        [HttpGet(RoutesHelper.ServiceOrders.GetAverage)]
        [ProducesResponseType(typeof(TimeSpan?), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TimeSpan?), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public ActionResult<TimeSpan> GetAverage()
        {
            TimeSpan? time = _serviceOrdersService.GetAverageTime(new());

            return Ok(time);
        }
    }
}