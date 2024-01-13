using Dapper;
using Npgsql;
using portfolio.Models;

namespace portfolio.Repositories;

public class PersonalInfoRepository
{
    private readonly IConfiguration _config;
    private readonly string? _connectionString;

    public PersonalInfoRepository(IConfiguration config)
    {
        _config = config;
        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (env == "Development")
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        if (env == "Production")
        {
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
    }

    public async Task<PersonalInfoModel> GetPersonalInfo()
    {
        await using var connection = new NpgsqlConnection(_connectionString);

        var sql = @"
            SELECT *
            FROM personal_info
        ";

        var result = await connection.QueryAsync<PersonalInfoModel>(sql);
        return result.FirstOrDefault();
    }
}
