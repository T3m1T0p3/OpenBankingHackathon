using Duende.IdentityServer.Models;
using static Duende.IdentityServer.Models.IdentityResources;

namespace IdsServer.Model
{
    public class IdentityResourceDto
    {
        public Profile ProfileIdentity { get; set; } = new Profile();
        public OpenId  OpenIdIdentity{ get; set;} = new OpenId();
        public ICollection<string> UserClaims { get; set; }
        public ICollection<string> Name { get; set; }
    }
}
