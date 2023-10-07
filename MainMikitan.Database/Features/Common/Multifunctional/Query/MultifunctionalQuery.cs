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
}