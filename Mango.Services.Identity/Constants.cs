using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace Mango.Services.Identity
{
    public static class Constants
    {
        public static class Roles
        {
            public const string ADMIN = "Admin";
            public const string CUSTOMER = "Customer";
        }
        
        public static class Identity
        {
            public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

            public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
            {
                new ApiScope("mango", "Mango Server"),
                new ApiScope("read", displayName: "Read data."),
                new ApiScope("write", displayName: "Write data."),
                new ApiScope("delete", displayName: "Delete data.")
            };

            public static IEnumerable<Client> Clients => new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "mango",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44369/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44369/signout-callback-oidc" },
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
                }
            };
        }
    }
}
