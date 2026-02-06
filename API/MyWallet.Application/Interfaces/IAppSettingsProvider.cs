namespace MyWallet.Application.Interfaces
{
    public interface IAppSettingsProvider
    {
        string GetJwtSigningKey();
        string GetJwtIssuer();
        string GetJwtAudience();
        int GetJwtExpirationTimeInMinutes();
    }
}
