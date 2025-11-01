using FIAPOficina.Application.Common.Security;
using FIAPOficina.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Mocks.Common
{
    internal class PasswordHasherMock : IPasswordHasherService
    {
        public string Hash(string password)
        {
            return password;
        }

        public bool Verify(string password, string hashedPassword)
        {
            return true;
        }
    }
}
