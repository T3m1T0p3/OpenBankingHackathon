using Duende.IdentityServer.Models;

namespace IdsServer.Model
{
    public class ResourceDto: BaseModel<ResourceDto>
    {
        public string ResourceName { get; set; }
        public List<string> Scopes { get; set; } = new List<string>();
        public List<string> Secrets { get; set; }=new List<string>();
        public List<string> UserClaims { get; set; }= new List<string>();
    }
}
