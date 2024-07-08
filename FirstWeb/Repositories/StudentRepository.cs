using Dapper;
using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;


namespace FirstWeb.Repositories
{
    public class StudentRepository : IStudentRepository<Student>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public StudentRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connection = SqlConnectionSingleton.GetInstance(configuration);
        }
        public async Task AddAsync(Student entity)
        {
            var query = @"INSERT INTO Students(MaSV, HoTen, GioiTinh, NgaySinh, SoDiemCong)
                        VALUES (@MaSV, @HoTen, @GioiTinh, @NgaySinh, @SoDiemCong)";
            var parameters = new
            {
                MaSV = entity.maSV,
                HoTen = entity.hoTen,
                GioiTinh = entity.gioiTinh,
                NgaySinh = entity.ngaySinh,
                SoDiemCong = entity.soDiemCong
            };
            await _connection.ExecuteAsync(query, parameters);
        }

        public async Task DeleteAsync(string id)
        {
            var query = @"DELETE FROM Students WHERE MaSV = @maSV";
            var parmeter = new
            {
                maSV = id
            };
            await _connection.ExecuteAsync(query, parmeter);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var query = "SELECT * FROM Students";
            var result = await _connection.QueryAsync<Student>(query);
            return result;
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            var query = "SELECT * FROM Students WHERE MaSV = @maSV";
            var parameter = new { maSV = id };
            return await _connection.QuerySingleOrDefaultAsync<Student>(query, parameter);
        }

        public async Task UpdateAsync(Student entity)
        {
            var query = @"UPDATE Students 
                            SET HoTen = @hoTen,
                                GioiTinh = @gioiTinh, 
                                NgaySinh = @ngaySinh,
                                SoDiemCong = @soDiemCong
                            Where MaSV = @maSV";
            var parameter = new
            {
                hoTen = entity.hoTen,
                gioiTinh = entity.gioiTinh,
                ngaySinh = entity.ngaySinh,
                soDiemCong = entity.soDiemCong,
                maSV = entity.maSV
            };
            await _connection.ExecuteAsync(query, parameter);
        }
    }
}
