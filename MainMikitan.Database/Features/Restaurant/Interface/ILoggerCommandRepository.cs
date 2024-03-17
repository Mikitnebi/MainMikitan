namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface ILoggerCommandRepository
{
    public Task AddLogInDb(Exception exception, string methodName, string request, string response);
    public Task AddLog(string data, string apiName);
}