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
            var query = @"INSERT INTO Students(Image, MaSV, HoTen, GioiTinh, NgaySinh, SoDiemCong, MaNganh)
                        VALUES (@Image, @MaSV, @HoTen, @GioiTinh, @NgaySinh, @SoDiemCong, @MaNganh)";
            var parameters = new
            {
                Image = entity.image,
                MaSV = entity.maSV,
                HoTen = entity.hoTen,
                GioiTinh = entity.gioiTinh,
                NgaySinh = entity.ngaySinh,
                SoDiemCong = entity.soDiemCong,
                maNganh = entity.maNganh
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
            var query = "SELECT * FROM Students ";
            
            var result = await _connection.QueryAsync<Student>(query);
            return result;
        }

        public async Task<IEnumerable<Student>> GetAllItemAsync(int index)
        {
            int offset = (index > 0) ? (index - 1) * 4 : 0;
            var query = @"SELECT* FROM Students ORDER BY maSV OFFSET @Offset ROWS FETCH NEXT 4 ROWS ONLY";
            var parameter = new { Offset = offset };
            var result = await _connection.QueryAsync<Student>(query,parameter);
            return result;
        }

        public async Task<int> GetNumItemAsync()
        {
            var query = "SELECT COUNT(*) FROM Students";

            // Thực hiện truy vấn bằng Dapper
            var result = await _connection.QueryFirstOrDefaultAsync<int>(query);

            return result;
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            var query = "SELECT * FROM Students WHERE MaSV = @maSV";
            var parameter = new { maSV = id };
            return await _connection.QuerySingleOrDefaultAsync<Student>(query, parameter);
        }

        public async Task<IEnumerable<Student>> GetSearchMaNganhAsync(string maNganh)
        {
            var query = "SELECT * FROM Students WHERE MaNganh = @maNganh";
            var parameters = new { maNganh = maNganh };
            var majors = await _connection.QueryAsync<Student>(query, parameters);
            return majors.ToList();
        }

        public async Task<IEnumerable<Student>> GetSearchNameAsync(string name)
        {
            var query = "SELECT * FROM Students WHERE HoTen LIKE '%' + @name + '%'";
            var parameter = new { name = name };
            var students = await _connection.QueryAsync<Student>(query, parameter);
            return students.ToList();
        }

        public async Task UpdateAsync(Student entity)
        {
            var query = @"UPDATE Students 
                            SET Image = @image,
                                HoTen = @hoTen,
                                GioiTinh = @gioiTinh, 
                                NgaySinh = @ngaySinh,
                                SoDiemCong = @soDiemCong,
                                MaNganh = @maNganh
                            WHERE MaSV = @maSV";
            var parameter = new
            {
                Image = entity.image,
                hoTen = entity.hoTen,
                gioiTinh = entity.gioiTinh,
                ngaySinh = entity.ngaySinh,
                soDiemCong = entity.soDiemCong,
                maNganh = entity.maNganh,
                maSV = entity.maSV
            };
            await _connection.ExecuteAsync(query, parameter);
        }
        public async Task<int> GetNumMarjorItemAsync(string id)
        {
            var query = @"SELECT COUNT(*) FROM Students WHERE MaNganh = @maNganh";
            var parameter = new
            {
                maNganh = id
            };
            // Thực hiện truy vấn bằng Dapper
            var result = await _connection.QueryFirstOrDefaultAsync<int>(query, parameter);

            return result;
        }
        public async Task<IEnumerable<Student>> GetAllMajorAsync(string maNganh, int index)
        {
            int offset = (index > 0) ? (index - 1) * 4 : 0;
            var query = @"SELECT * FROM Students WHERE MaNganh = @maNganh ORDER BY MaNganh OFFSET @Offset ROWS FETCH NEXT 4 ROWS ONLY";
            var parameter = new
            {
                maNganh = maNganh,
                Offset = offset
            };
            var result = await _connection.QueryAsync<Student>(query, parameter);
            return result;
        }
    }
}
