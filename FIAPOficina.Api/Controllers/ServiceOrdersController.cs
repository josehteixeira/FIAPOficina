using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.ServiceOrders.Requests;
using FIAPOficina.Api.Models.ServiceOrders.Responses;
using FIAPOficina.Application.ServiceOrders.Services;
using FIAPOficina.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
    }
}