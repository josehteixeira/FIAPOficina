using FIAPOficina.Api.Helpers;
using FIAPOficina.Api.Models.Users.Requests;
using FIAPOficina.Api.Models.Users.Responses;
using FIAPOficina.Application.Users.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

            return Created((Uri)null!, new UserResponse(
                user.Id, 
                user.Name, 
                user.UserName
            ));
        }


        [Authorize]
        [HttpPut(RoutesHelper.Users.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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


        [Authorize]
        [HttpDelete(RoutesHelper.Users.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _usersService.DeleteAsync(new(id));

            return Ok();
        }

        [Authorize]
        [HttpGet(RoutesHelper.Users.GetSingle)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<UserResponse>> GetSingle([FromRoute] Guid id)
        {
            var user = await _usersService.GetSingleAsync(new(id));

            return Ok(user);
        }

        [Authorize]
        [HttpGet(RoutesHelper.Users.GetAll)]
        [ProducesResponseType(typeof(UserResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public ActionResult<UserResponse[]> GetAll()
        {
            var users = _usersService.GetAll(new());

            if (users is not null && users.Length > 0)
            {
                return Ok(users.Select(user => new UserResponse(
                    user.Id, 
                    user.Name, 
                    user.UserName
                )).ToArray());
            }

            return Ok(Array.Empty<UserResponse>());
        }
    }
}