using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Reflection;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Query;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.InternalServiceAdapterService.Exceptions;
using Microsoft.Extensions.Options;
using NPOI.SS.Formula.Functions;

namespace MainMikitan.Database.Features.Common.Multifunctional.Repository;

public class MultifunctionalRepository : IMultifunctionalRepository
{
    private readonly ConnectionStringsOptions _connectionString;
    private readonly IMultifunctionalQuery _multifunctionalQuery;

    public MultifunctionalRepository(
        IMultifunctionalQuery multifunctionalQuery, 
        IOptions<ConnectionStringsOptions> connectionString)
    {
        _multifunctionalQuery = multifunctionalQuery;
        _connectionString = connectionString.Value;
    }

    public async Task AddOrUpdateTableData<T>(T model) where T : class
    {
        using IDbConnection connection = new SqlConnection(_connectionString.MainMik);
        
        if (typeof(T).BaseType is null || typeof(T).BaseType?.Name != "MultifunctionalQueryMainModel")
        {
            throw new MainMikitanException($"{typeof(T)} Class must be child of MultifunctionalQueryMainModel", "AddOrUpdateTableData");
        }
        
        var properties =
            typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        
        var databaseName = properties.FirstOrDefault(p => p.Name == "DatabaseName");
        var schemaName = properties.FirstOrDefault(p => p.Name == "SchemaName");
        var tableNameData = properties.FirstOrDefault(p => p.Name == "TableName");
        var tableName = $"[{databaseName.GetValue(model)}].[{schemaName.GetValue(model)}].[{tableNameData.GetValue(model)}]";
        var id = properties.FirstOrDefault(i => i.Name == "Id");
        
        if (id.GetValue(model) is null)
        {
            var createSql = _multifunctionalQuery.GenerateCreateQuery(properties, tableName);
            
            await connection.QueryAsync(createSql, model);
            
            return;
        }
        
        properties = properties.Where(prop =>
        {
            var value = prop.GetValue(model, null);
            return value != null;
        }).ToArray();

        var updateQuery = _multifunctionalQuery.GenerateUpdateQuery(properties, tableName);

        await connection.QueryAsync(updateQuery, model);
    }

    public async Task CreateOrUpdateTable<T>(List<T> model) where T : class
    {
        using IDbConnection connection = new SqlConnection(_connectionString.MainMik);
        
        if (typeof(T).BaseType is null || typeof(T).BaseType?.Name != "MultifunctionalQueryMainModel")
        {
            throw new MainMikitanException($"{typeof(T)} Class must be child of MultifunctionalQueryMainModel", "CreateOrUpdateTable");
        }
        var tableExistsQuery = _multifunctionalQuery.TableExistsQuery();
        
        var properties =
            typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        
        var databaseName = properties.FirstOrDefault(p => p.Name == "DatabaseName");
        var schemaName = properties.FirstOrDefault(p => p.Name == "SchemaName");
        var tableName = typeof(T).Name;
        
        var tableExists = await connection.QueryFirstOrDefaultAsync<bool>(tableExistsQuery, new
        {
            schema = schemaName,
            table = tableName,
            database = databaseName
        });

        if (tableExists)
        {
            throw new MainMikitanException("Table Already Exists", "CreateOrUpdateTable");
        }

        tableName = $"[{databaseName}].[{schemaName}].[{tableName}]";

        var tableCreateQuery = _multifunctionalQuery.GenerateCreateTableQuery(tableName, properties);
        
        await connection.ExecuteAsync(tableCreateQuery, model);
    }
}