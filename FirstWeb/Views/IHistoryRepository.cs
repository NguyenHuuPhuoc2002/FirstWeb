namespace FirstWeb.Views
{
    public interface IHistoryRepository<T>
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
