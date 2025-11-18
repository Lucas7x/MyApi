namespace MyWallet.Application.Interfaces
{
    public interface IAuthenticateService
    {
        bool Authenticate(string email, string password);
        bool UserExists(string email);
        string GenerateToken(int id, string email);
    }
}
