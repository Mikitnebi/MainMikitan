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
    protected ResponseModel<T> Unexpected(string errorMessage)
    {
        return new ResponseModel<T>
        {
            ErrorMessage = errorMessage,
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
    protected ResponseModel<bool> Unexpected(string errorMessage)
    {
        return new ResponseModel<bool>
        {
            ErrorMessage = errorMessage,
            ErrorType = ErrorType.UnExpectedException
        };
    }
    protected ResponseModel<bool> Success()
    {
        return new ResponseModel<bool> { Result = true };
    }
}