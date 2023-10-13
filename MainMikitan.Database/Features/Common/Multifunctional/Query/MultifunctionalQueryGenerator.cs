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
            if (property.Name is not "Id")
            {
                createSql += $"{property.Name}, ";
            }
        }
        
        createSql = $"{createSql.Trim().TrimEnd(',', ')')})";
        createSql += " VALUES (";
            
        foreach (var property in properties)
        {
            if (property.Name is not "Id")
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
            if (property.Name is not "Id")
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
            if (column.ColumnName is "Id")
            {
                return $"{column.ColumnName} {column.DataType} IDENTITY( 1, 1)";
            }

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
              "AND table_name = @table " +
              "AND table_catalog = @database)) " +
              "BEGIN SELECT 1; END " +
              "ELSE BEGIN SELECT 0; END";
    }
    
    private string GetSqlTypeFromPropertyType(Type propertyType)
    {
        if (propertyType == typeof(int) || propertyType == typeof(int?)) return "INT";
        if (propertyType == typeof(double) || propertyType == typeof(double?)) return "DECIMAL";
        if (propertyType == typeof(string)) return "NVARCHAR(MAX)";
        if (propertyType == typeof(long) || propertyType == typeof(long?)) return "BIGINT";
        if (propertyType == typeof(bool) || propertyType == typeof(bool?)) return "BIT";
        if (propertyType == typeof(short) || propertyType == typeof(short?)) return "SMALLINT";
        if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?)) return "DATETIME";
        if (propertyType == typeof(DateOnly) || propertyType == typeof(DateOnly?)) return "DATE";
        return propertyType.Name;
    }
    
    private bool IsPropertyNullable(Type propertyType)
    {
        return Nullable.GetUnderlyingType(propertyType) != null;
    }
}