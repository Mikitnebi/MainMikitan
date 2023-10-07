using System.Reflection;

namespace MainMikitan.Database.Features.Common.Multifunctional.Interface.Query;

public interface IMultifunctionalQuery
{
    public string GenerateCreateQuery(PropertyInfo[] properties, string tableName);
    public string GenerateUpdateQuery(PropertyInfo[] properties, string tableName);
}