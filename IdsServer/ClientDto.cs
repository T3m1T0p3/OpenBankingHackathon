using Duende.IdentityServer.EntityFramework.Entities;
using IdsServer.Model;
using Newtonsoft.Json;

namespace IdsServer
{
    public class ClientDto:BaseModel<ClientDto>
    {
        [JsonProperty("ClientId")]
        public string ClientId { get; set; }
        [JsonProperty("ClientName")]
        public string ClientName { get; set; }
        //public List<string> AllowedGtrantType { get; set; }
        //public List<string> Scopes { get; set; }
        //public IEnumerable<Secret> ClientSecrets { get; set; }
        //public string GrantType { get; set; }
    }
}
