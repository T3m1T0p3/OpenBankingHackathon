namespace OpenBankingCore.RepositoryService
{
    public interface IRepositoryService<T> where T : class
    {
        public void Add();
        public void Delete();
        public void Update();
    }
}
