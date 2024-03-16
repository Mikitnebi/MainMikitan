using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Logger;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class LoggerCommandRepository(MikDbContext dbContext) : ILoggerCommandRepository
{
    public async Task AddLogInDb(Exception exception, string methodName, string request, string response)
    {
        var loggerEntity = new LoggerEntity()
        {
            Request = request,
            Response = response,
            MethodName = methodName,
            Exception = exception.Message,
            StackTrace = exception.StackTrace,
            Data = exception.Data.ToString(),
            ThrowTime = DateTimeOffset.Now
        };

        await dbContext.Logger.AddAsync(loggerEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddLog(string data, string methodName)
    {
        var loggerEntity = new LoggerEntity
        {
            MethodName = methodName,
            Data = data,
            ThrowTime = DateTimeOffset.Now
        };

        await dbContext.Logger.AddAsync(loggerEntity);
        await dbContext.SaveChangesAsync();
    }
}