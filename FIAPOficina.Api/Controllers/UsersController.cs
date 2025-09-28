using FIAPOficina.Api.Helpers;
using FIAPOficina.Application.Users.Commands.CreateUser;
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
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        public async Task<ActionResult<User>> Create([FromBody] CreateUserCommand command)
        {
            var user = await _usersService.AddAsync(new(
                Name: command.Name,
                UserName: command.UserName,
                Password: command.Password
            ));

            return Created((Uri)null!, user);
        }
    }
}