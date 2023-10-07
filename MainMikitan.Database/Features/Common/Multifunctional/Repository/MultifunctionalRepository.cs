using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Reflection;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Query;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Domain.Models.Setting;

namespace MainMikitan.Database.Features.Common.Multifunctional.Repository;

public class MultifunctionalRepository : IMultifunctionalRepository
{
    private readonly ConnectionStringsOptions _connectionString;
    private readonly IMultifunctionalQuery _multifunctionalQuery;

    public MultifunctionalRepository(
        IMultifunctionalQuery multifunctionalQuery, 
        ConnectionStringsOptions connectionString)
    {
        _multifunctionalQuery = multifunctionalQuery;
        _connectionString = connectionString;
    }

    public async Task AddOrUpdateTableData<T>(T model) where T : class
    {
        using IDbConnection connection = new SqlConnection(_connectionString.MainMik);
        
        if (typeof(T).BaseType is null || typeof(T).BaseType?.Name != "MultifunctionalQueryMainModel")
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
            var createSql = _multifunctionalQuery.GenerateCreateQuery(properties, tableName);
            
            await connection.ExecuteAsync(createSql, model);
            
            return;
        }

        var updateQuery = _multifunctionalQuery.GenerateUpdateQuery(properties, tableName);

        await connection.ExecuteAsync(updateQuery, model);
    }
}