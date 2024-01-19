using MainMikitan.Domain.Models.Commons;

namespace MainMikitan.InternalServicesAdapter.HandlerResponseMaker;

public static class HandlerResponseMakerService
{
    public static ResponseModel<bool> NewFailedResponse(string errorType, string? errorMessage = null)
    {
        return new ResponseModel<bool>
        {
            ErrorMessage = errorMessage,
            ErrorType = errorType
        };
    }
    
    public static ResponseModel<T> NewFailedResponse<T>(string errorType, string? errorMessage = null)
    {
        return new ResponseModel<T>
        {
            ErrorMessage = errorMessage,
            ErrorType = errorType
        };
    }
    
    public static ResponseModel<bool> NewSucceedResponse()
    {
        return new ResponseModel<bool> { Result = true };
    }
    
    public static ResponseModel<T> NewSucceedResponse<T>(T result)
    {
        return new ResponseModel<T> { Result = result };
    }
}