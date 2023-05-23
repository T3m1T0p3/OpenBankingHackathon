using ClientProject.Model;

namespace ClientManager.Services.TokenServices
{
    public interface ITokenService
    {
        public string GetToken(string signingKey,string issuer, OpenBankingClient client);
        public bool ValidateToken(string signingKey, string issuer, string audience, string token);
    }
}
