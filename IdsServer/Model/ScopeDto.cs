namespace IdsServer.Model
{
    public class ScopeDto:BaseModel<ScopeDto>
    {
        public List<string> Scopes { get; set;} = new List<string> ();
    }
}
