using MainMikitan.Domain.Models.Commons;

namespace MainMikitan.Domain.Templates;

public class ResponseMaker<T>
{
    protected ResponseModel<T> Fail(string errorType)
    {
        return new ResponseModel<T>
        {
            ErrorType = errorType
        };
    }
    protected ResponseModel<T> Unexpected(Exception ex)
    {
        return new ResponseModel<T>
        {
            ErrorMessage = ex.Message,
            ErrorType = ErrorType.UnExpectedException
        };
    }
    
    protected ResponseModel<T> Success(T result)
    {
        return new ResponseModel<T> { Result = result };
    }
}
public class ResponseMaker
{
    protected ResponseModel<bool> Fail(string errorType)
    {
        return new ResponseModel<bool>
        {
            ErrorType = errorType
        };
    }
    protected static ResponseModel<bool> Unexpected(Exception ex)
    {
        return new ResponseModel<bool>
        {
            ErrorMessage = ex.Message,
            ErrorType = ErrorType.UnExpectedException
        };
    }
    protected ResponseModel<bool> Success()
    {
        return new ResponseModel<bool> { Result = true };
    }
}