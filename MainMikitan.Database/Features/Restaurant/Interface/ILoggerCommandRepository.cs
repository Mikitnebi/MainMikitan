namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface ILoggerCommandRepository
{
    public Task AddLogInDb(Exception exception, string methodName);
    public Task AddLog(string data, string apiName);
}