using FIAPOficina.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Common.Security
{
    public  class PasswordHasherServiceTests
    {
        [Fact]
        public void Should_Create_Hash_From_Password()
        {
            var service = new PasswordHasherService();
            var hash = service.Hash("Batatinha");

            Assert.NotNull(hash);
        }

        [Fact]
        public void Should_Validate_Hash_From_Password()
        {
            var service = new PasswordHasherService();
            var hash = service.Hash("Batatinha");

            Assert.NotNull(hash);
            Assert.True(service.Verify("Batatinha",hash));
        }
    }
}
