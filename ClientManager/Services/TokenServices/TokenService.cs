using ClientProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClientManager.Services.TokenServices
{
    public class TokenService : ITokenService
    {
        public string GetToken(string signingKey, string issuer, OpenBankingClient client)
        {
            Console.WriteLine("Creating claims");
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, client.Email),
                new Claim(ClaimTypes.Name, client.ClientName)
            };
            Console.WriteLine("Creating creds");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer,"https://localhost:3000",claims,DateTime.Now, DateTime.Now.AddMinutes(60), credentials);
            Console.WriteLine(tokenDescriptor.RawPayload);
            var tokenCreator = new JwtSecurityTokenHandler();
            string token= tokenCreator.WriteToken(tokenDescriptor);
            Console.WriteLine(token.Length);
            return token;
        }
        public bool ValidateToken(string signingKey, string issuer, string audience, string token)
        {

            var mySecret = Encoding.UTF8.GetBytes(signingKey);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
        
}
