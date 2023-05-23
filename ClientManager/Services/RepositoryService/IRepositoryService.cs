using ClientProject.Model;

namespace ClientManager.Services.RepositoryService
{
    public interface IRepositoryService<T> where T : class
    {
        public void Add();
        public void Delete();
        public void Update();
        public OpenBankingClient Get(string email);
    }
}
