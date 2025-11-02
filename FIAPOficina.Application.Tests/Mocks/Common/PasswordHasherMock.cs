using FIAPOficina.Application.Common.Security;

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
