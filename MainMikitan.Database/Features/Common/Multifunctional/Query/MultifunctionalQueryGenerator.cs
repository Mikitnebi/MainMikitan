using System.Reflection;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Query;

namespace MainMikitan.Database.Features.Common.Multifunctional.Query;

public class MultifunctionalQuery : IMultifunctionalQuery
{
    public string GenerateCreateQuery(PropertyInfo[] properties, string tableName)
    {
        var createSql = $"INSERT INTO {tableName} (";
        
        foreach (var property in properties)
        {
            if (property.Name is not ("Id" or "DatabaseName" or "SchemaName" or "TableName"))
            {
                createSql += $"{property.Name}, ";
            }
        }
        
        createSql = $"{createSql.Trim().TrimEnd(',', ')')})";
        createSql += " VALUES (";
            
        foreach (var property in properties)
        {
            if (property.Name is not ("Id" or "DatabaseName" or "SchemaName" or "TableName"))
            {
                createSql += $"@{property.Name}, ";
            }
        }
            
        createSql = $"{createSql.Trim().TrimEnd(',', ')')})";


        return createSql;
    }
    
    public string GenerateUpdateQuery(PropertyInfo[] properties, string tableName)
    {
        var updateSql = $"UPDATE {tableName} SET ";

        foreach (var property in properties)
        {
            if (property.Name is not ("Id" or "DatabaseName" or "SchemaName" or "TableName"))
            {
                updateSql += $"{property.Name} = @{property.Name}, ";
            }
        }
        
        updateSql = updateSql.Trim().TrimEnd(',', ' ');

        updateSql += " WHERE Id = @Id";

        return updateSql;
    }

    public string GenerateCreateTableQuery(string tableName, PropertyInfo[] properties)
    {
        dynamic[] columns = properties.Select(propertyInfo => new
        {
            ColumnName = propertyInfo.Name,
            DataType = GetSqlTypeFromPropertyType(propertyInfo.PropertyType),
            IsNullable = IsPropertyNullable(propertyInfo.PropertyType)
        }).ToArray();
        
        string createTableSQL = $"CREATE TABLE {tableName} (";
        createTableSQL += string.Join(", ", columns.Select(column =>
        {
            if (column.ColumnName is not ("DatabaseName" or "SchemaName" or "TableName"))
            {
                string nullable = column.IsNullable ? "NULL" : "NOT NULL";
                return $"{column.ColumnName} {column.DataType} {nullable}";
            }
            else if (column.ColumnName is "Id")
            {
                string nullable = column.IsNullable ? "NULL" : "NOT NULL";
                return $"{column.ColumnName} {column.DataType} {nullable} IDENTITY( 1, 1)";
            }

            return "";
        }));
        createTableSQL += ");";

        return createTableSQL;
    }
    
    public string TableExistsQuery()
    {
        return "IF (EXISTS (SELECT 1 FROM MainMikitan.information_schema.tables " +
              "WHERE table_schema = @schema " +
              "AND table_name = @table " +
              "AND table_catalog = @database)) " +
              "BEGIN SELECT 1; END " +
              "ELSE BEGIN SELECT 0; END";
    }
    
    private string GetSqlTypeFromPropertyType(Type propertyType)
    {
        if (propertyType == typeof(string)) return "NVARCHAR(MAX)";
        else if (propertyType == typeof(long)) return "BIGINT";
        else if (propertyType == typeof(bool)) return "BIT";
        else if (propertyType == typeof(short)) return "SMALLINT";

        return propertyType.ToString();
    }
    
    private bool IsPropertyNullable(Type propertyType)
    {
        return Nullable.GetUnderlyingType(propertyType) != null;
    }
}