using Dapper;
using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FirstWeb.Repositories
{
    public class LoginRepository : ILoginRepository<Login>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public LoginRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connection = SqlConnectionSingleton.GetInstance(configuration);
        }


        public async Task<Login> checkLogin(string taiKhoan, string matKhau)
        {
            var query = "SELECT * FROM Login WHERE TenDangNhap = @tenDangNhap AND MatKhau = @matKhau AND Role = @role";
            var parameter = new
            {
                tenDangNhap = taiKhoan,
                matKhau = matKhau,
                role = 1
            };

            var result = await _connection.QueryFirstOrDefaultAsync(query, parameter);

            // Kiểm tra kết quả và ánh xạ sang đối tượng Login nếu có
            if (result != null)
            {
                var login = new Login
                {
                    taiKhoan = result.TenDangNhap,
                    matKhau = result.MatKhau,
                        
                };
                return login;
            }
            return null; 
          
        }

        public async Task registerUser(string taiKhoan, string matKhau)
        {
            var query = "INSERT INTO Login(TenDangNhap, MatKhau, Role)" +
                "                   VALUES(@taiKhoan, @matKhau, @role)";
            var parameter = new
            {
                taiKhoan = taiKhoan,
                matKhau = matKhau,
                role = 1
            };
            await _connection.ExecuteAsync(query, parameter);
        }
    }
}
