using Duende.IdentityServer.Models;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServerWeb
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new IdentityResource[] {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId(),
                new IdentityResource
                {
                    Name="creditor",
                    UserClaims=new[] {"creditor"}
                },
                new IdentityResource
                {
                    Name="user",
                    UserClaims=new[] {"user"}
                }
            };
        }

        public static IEnumerable<ApiScope> ApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("write"),
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new Client[] {
                new Client
                {
                ClientId = "creditor",
                ClientName = "Creditor",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "write" }
                },

            // interactive client using code flow + pkce
            new Client
                {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile"}
                }
            };
        }

        public static IEnumerable<ApiResource> ApiResources()
        {
            return new ApiResource[]
            {
                new ApiResource("creditorAPI")
                {
                    Scopes= new List<string>{ "write"},
                    ApiSecrets=new List<Secret> {new Secret("Api Secret".Sha256())},
                    UserClaims= new List<string>{"role"}
                }
            };
        }
    }
}
