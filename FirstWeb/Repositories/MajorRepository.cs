using Dapper;
using FirstWeb.Models;
using FirstWeb.Views;
using Microsoft.Data.SqlClient;

namespace FirstWeb.Repositories
{
    public class MajorRepository : IMajorRepository<Major>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        public MajorRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connection = SqlConnectionSingleton.GetInstance(configuration);
        }
        public async Task<Major> GetByIdAsync(string maNganh)
        {
            var query = "SELECT * FROM Majors WHERE maNganh = @maNganh";
            var parameter = new { maNganh = maNganh };
            return await _connection.QuerySingleOrDefaultAsync<Major>(query, parameter);
        }

        public async Task<IEnumerable<Major>> GetNganhAsync()
        {
            var query = "SELECT * FROM Majors";
            var result = await _connection.QueryAsync<Major>(query);
            return result;
        }
    }
}
