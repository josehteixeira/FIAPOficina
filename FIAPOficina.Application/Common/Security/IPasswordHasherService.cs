namespace FIAPOficina.Application.Common.Security
{
    public interface IPasswordHasherService
    {
        string Hash(string password);
        bool Verify(string password, string hashedPassword);
    }
}