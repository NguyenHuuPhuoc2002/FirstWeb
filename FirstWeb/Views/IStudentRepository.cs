namespace FirstWeb.Views
{
    public interface IStudentRepository<T>
    {

        Task<IEnumerable<T>> GetAllItemAsync(int index);
        Task<int> GetNumItemAsync();

        Task<int> GetNumMarjorItemAsync(string id);
        Task<IEnumerable<T>> GetAllMajorAsync(string maNganh, int index);

        Task<int> GetTotalNameAsync(string name);
        Task<IEnumerable<T>> GetAllNameItemAsync(string name, int index);

        Task<T> GetByIdAsync(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
 
    }
}
