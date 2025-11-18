using MyWallet.Application.Interfaces;

namespace MyApi.Utils
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        private readonly IConfiguration _configuration;

        public AppSettingsProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetJwtAudience()
        {
            string? audience = _configuration.GetSection("Jwt")["Audience"];
            if (string.IsNullOrEmpty(audience))
                throw new ArgumentNullException(nameof(audience));

            return audience;
        }

        public string GetJwtIssuer()
        {
            string? issuer = _configuration.GetSection("Jwt")["Issuer"];
            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentNullException(nameof(issuer));

            return issuer;
        }

        public string GetJwtSigningKey()
        {
            string? signingKey = _configuration.GetSection("Jwt")["IssuerSigningKey"];
            if (string.IsNullOrEmpty(signingKey))
                throw new ArgumentNullException(nameof(signingKey));

            return signingKey;
        }
    }
}
