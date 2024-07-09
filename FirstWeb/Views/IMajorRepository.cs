namespace FirstWeb.Views
{
    public interface IMajorRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetNganhAsync();
    }
}
