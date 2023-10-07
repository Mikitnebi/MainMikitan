using System.Diagnostics;

namespace MainMikitan.InternalServiceAdapterService.Exceptions;

public class MainMikitanException : Exception
{
    public string FunctionName { get; }
    public StackTrace StackTrace { get; }
    
    public MainMikitanException(){ }
    
    public MainMikitanException(string message) : base(message) { }
    
    public MainMikitanException(string message, Exception innerException) : base(message, innerException) { }

    public MainMikitanException(string message, string functionName) : base(message)
    {
        FunctionName = functionName;
    }

    public MainMikitanException(string message, string functionName , Exception innerException) : base(message, innerException)
    {
        FunctionName = functionName;
    }
    
    public MainMikitanException(string message, string functionName, StackTrace stackTrace) : base(message)
    {
        FunctionName = functionName;
        StackTrace = stackTrace;
    }
    
    public MainMikitanException(string message, string functionName, StackTrace stackTrace, Exception exception) : base(message, exception)
    {
        FunctionName = functionName;
        StackTrace = stackTrace;
    }
}