namespace OpenBankingCore.RepositoryService
{
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private ApplicationDbContext _context;

        public RepositoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add()
        {
            throw new Exception("Not Implemented");
        }

        public void Delete()
        {
            throw new Exception("Not Implemented");
        }

        public void Update()
        {
            throw new Exception("Not Implemented");
        }
    }
}
