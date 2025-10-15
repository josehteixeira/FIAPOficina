using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.Users.Requests;
using FIAPOficina.Api.Models.Users.Responses;
using FIAPOficina.Application.Users.Services;
using FIAPOficina.Domain.Users.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FIAPOficina.Api.Controllers
{
    [ApiController]
    [Route(RoutesHelper.Users.Controller)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost(RoutesHelper.Users.Create)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<UserResponse>> Create([FromBody] CreateUserRequest request)
        {
            var user = await _usersService.AddAsync(new(
                Name: request.Name,
                UserName: request.UserName,
                Password: request.Password
            ));

            return Created((Uri)null!, user);
        }


        [HttpPut(RoutesHelper.Users.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request, [FromRoute] Guid id)
        {
            var user = await _usersService.UpdateAsync(new(
                Id: id,
                Name: request.Name,
                UserName: request.UserName
            ));

            return NoContent();
        }


        [HttpDelete(RoutesHelper.Users.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _usersService.DeleteAsync(new(id));

            return Ok();
        }
    }
}