namespace ApiResource.Repository
{
    public interface IRepositoryService
    {
        public void Add<T>();
        public T Get<T>();
        public void Delete<T>();
        public void Update<T>();
    }
}
