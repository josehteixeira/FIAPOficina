namespace FIAPOficina.Application.Common.Security
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private readonly Microsoft.AspNetCore.Identity.PasswordHasher<object> _hasher
            = new Microsoft.AspNetCore.Identity.PasswordHasher<object>();

        public string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool Verify(string password, string hashedPassword)
        {
            var result = _hasher.VerifyHashedPassword(null!, hashedPassword, password);
            return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
        }
    }
}