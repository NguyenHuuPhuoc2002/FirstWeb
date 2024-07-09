using Dapper;
using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.Data.SqlClient;

namespace FirstWeb.Repositories
{
    public class HistoryRepository : IHistoryRepository<Student>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public HistoryRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connection = SqlConnectionSingleton.GetInstance(configuration);
        }

        public async Task AddAsync(Student entity)
        {
            var query = @"INSERT INTO History(MaSV, HoTen, GioiTinh, NgaySinh, SoDiemCong, MaNganh)
                        VALUES (@MaSV, @HoTen, @GioiTinh, @NgaySinh,@SoDiemCong, @MaNganh)";
            var parameters = new
            {
                MaSV = entity.maSV,
                HoTen = entity.hoTen,
                GioiTinh = entity.gioiTinh,
                NgaySinh = entity.ngaySinh,
                SoDiemCong = entity.soDiemCong,
                maNganh = entity.maNganh
            };
            await _connection.ExecuteAsync(query, parameters);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var query = "SELECT * FROM History";
            var result = await _connection.QueryAsync<Student>(query);
            return result;
        }
    }
}
