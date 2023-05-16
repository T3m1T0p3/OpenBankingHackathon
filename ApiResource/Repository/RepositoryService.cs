namespace ApiResource.Repository
{
    public class RepositoryService : IRepositoryService
    {

        ApplicationDbContext _dbContext;
        public RepositoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add<T>()
        {
            throw new NotImplementedException();
        }

        public void Delete<T>()
        {
            throw new NotImplementedException();
        }

        public T Get<T>()
        {
            throw new NotImplementedException();
        }

        public void Update<T>()
        {
            throw new NotImplementedException();
        }
    }
}
