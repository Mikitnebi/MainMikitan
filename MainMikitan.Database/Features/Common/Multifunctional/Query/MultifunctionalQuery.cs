using System.Reflection;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Query;

namespace MainMikitan.Database.Features.Common.Multifunctional.Query;

public class MultifunctionalQuery : IMultifunctionalQuery
{
    public string GenerateCreateQuery(PropertyInfo[] properties, string tableName)
    {
        var createSql = $"INSERT INTO {tableName} VALUES(";
            
        foreach (var property in properties)
        {
            if (property.Name is not ("Id" and "DatabaseName" and "SchemaName"))
            {
                createSql += $"@{property.Name}, ";
            }
        }
            
        createSql = createSql.TrimEnd(',', ')');

        return createSql;
    }
    
    public string GenerateUpdateQuery(PropertyInfo[] properties, string tableName)
    {
        var updateSql = $"UPDATE {tableName} SET ";

        foreach (var property in properties)
        {
            if (property.Name is not ("Id" and "DatabaseName" and "SchemaName"))
            {
                updateSql += $"{property.Name} = @{property.Name}, ";
            }
        }

        updateSql = updateSql.TrimEnd(',', ' ');

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
            string nullable = column.IsNullable ? "NULL" : "NOT NULL";
            return $"{column.ColumnName} {column.DataType} {nullable}";
        }));
        createTableSQL += ");";

        return createTableSQL;
    }
    
    public string TableExistsQuery()
    {
        return "IF (EXISTS (SELECT 1 FROM MainMikitan.information_schema.tables " +
              "WHERE table_schema = @schema " +
              "AND table_name = @table" +
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
        // Check if the type is nullable (e.g., int?)
        return Nullable.GetUnderlyingType(propertyType) != null;
    }
}