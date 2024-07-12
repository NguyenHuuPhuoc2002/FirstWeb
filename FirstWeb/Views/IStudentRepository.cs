namespace FirstWeb.Views
{
    public interface IStudentRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllItemAsync(int index);
        Task<int> GetAllNumItemAsync();
        Task<IEnumerable<T>> GetSearchMaNganhAsync(string maNganh);
        Task<IEnumerable<T>> GetSearchNameAsync(string name);
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
 
    }
}
