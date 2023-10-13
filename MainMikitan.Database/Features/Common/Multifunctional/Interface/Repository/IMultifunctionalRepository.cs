namespace MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;

public interface IMultifunctionalRepository
{
    public Task AddOrUpdateTableData<T>(T model) where T : class;
    public Task CreateOrUpdateTable<T>() where T : class;
}