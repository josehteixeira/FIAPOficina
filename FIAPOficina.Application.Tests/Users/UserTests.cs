using FIAPOficina.Application.Tests.Mocks.Common;
using FIAPOficina.Application.Tests.Mocks.Repositories;
using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Application.Users.Commands.DeleteUser;
using FIAPOficina.Application.Users.Commands.GetAllUsers;
using FIAPOficina.Application.Users.Commands.UpdateUser;
using FIAPOficina.Application.Users.Services;
using System.Collections.Immutable;

namespace FIAPOficina.Application.Tests.Users
{
    public class UserTests
    {
        private UserRepositoryMock _mock = new UserRepositoryMock();
        private PasswordHasherMock _pwMock = new PasswordHasherMock();

        [Fact]
        public void Should_Create_User()
        {
            var service = new UsersService(_mock,_pwMock);

            var user = service.AddAsync(new CreateUserCommand("Sergio","serginho.mec","MareaTurbo")).GetAwaiter().GetResult();
            Assert.NotNull(user);
            Assert.Equal("Sergio", user.Name);
            Assert.Equal("serginho.mec", user.UserName);
        }

        [Fact]
        public void Should_Delete_User()
        {
            var service = new UsersService(_mock,_pwMock);

            var user = service.AddAsync(new CreateUserCommand("Sergio","serginho.mec","MareaTurbo")).GetAwaiter().GetResult();

            var all = service.GetAll(new GetAllUsersCommand());

            foreach (var item in all)
                service.DeleteAsync(new DeleteUserCommand(item.Id));

            all = service.GetAll(new GetAllUsersCommand());
            Assert.Empty(all);
        }
    }
}
