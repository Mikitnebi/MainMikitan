namespace MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;

public interface IMultifunctionalRepository
{
    public Task AddOrUpdateTableData<T>(T model, string databaseName, string schemaName, string tableName) where T : class;
    public Task CreateOrUpdateTable<T>(string databaseName, string schemaName, string tableName) where T : class;
}