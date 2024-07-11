namespace FirstWeb.Views
{
    public interface ILoginRepository<T>
    {
        Task<T> checkLogin(string taiKhoan, string matKhau);
    }
}
