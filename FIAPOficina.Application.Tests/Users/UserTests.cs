using FIAPOficina.Application.Tests.Mocks.Common;
using FIAPOficina.Application.Tests.Mocks.Repositories;
using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Application.Users.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Users
{
    public class UserTests
    {
        private UsersRepositoryMock _mock = new UsersRepositoryMock();
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
    }
}
