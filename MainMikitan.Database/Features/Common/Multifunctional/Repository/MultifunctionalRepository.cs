using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Reflection;

namespace MainMikitan.Database.Features.Common.Multifunctional.Repository;

public class MultifunctionalRepository
{
    private readonly IDbConnection _dbConnection;

    public MultifunctionalRepository(string connectionString)
    {
        _dbConnection = new SqlConnection(connectionString);
    }

    public async Task AddOrUpdateTableData<T>(T model) where T : class
    {
        if (typeof(T).BaseType is null || typeof(T).BaseType.Name != "MultifunctionalQueryMainModel")
        {
            throw new Exception("Class must be child of MultifunctionalQueryMainModel");
        }
        
        var properties =
            typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

        var databaseName = properties.FirstOrDefault(p => p.Name == "DatabaseName");
        var schemaName = properties.FirstOrDefault(p => p.Name == "SchemaName");
        var tableName = $"[{databaseName}].[{schemaName}].[{typeof(T).Name}]";
        
        var id = properties.FirstOrDefault(i => i.Name == "Id");
        if (id is null)
        {
            var createSql = GenerateCreateQuery(properties, tableName);
            
            await _dbConnection.ExecuteAsync(createSql, model);
            
            return;
        }

        var updateQuery = GenerateUpdateQuery(properties, tableName);

        await _dbConnection.ExecuteAsync(updateQuery, model);
    }

    private string GenerateCreateQuery(PropertyInfo[] properties, string tableName)
    {
        var createSql = $"INSERT INTO {tableName} VALUES(";
            
        foreach (var property in properties)
        {
            if (property.Name != "Id")
            {
                createSql += $"@{property.Name}, ";
            }
        }
            
        createSql = createSql.TrimEnd(',', ')');

        return createSql;
    }

    private string GenerateUpdateQuery(PropertyInfo[] properties, string tableName)
    {
        var updateSql = $"UPDATE {tableName} SET ";

        foreach (var property in properties)
        {
            if (property.Name != "Id")
            {
                updateSql += $"{property.Name} = @{property.Name}, ";
            }
        }

        updateSql = updateSql.TrimEnd(',', ' ');

        updateSql += " WHERE Id = @Id";

        return updateSql;
    }
}