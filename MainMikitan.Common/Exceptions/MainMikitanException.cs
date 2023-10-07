using System.Diagnostics;

namespace MainMikitan.InternalServiceAdapterService.Exceptions;

public class MainMikitanException : Exception
{
    public string ClassName { get; }
    public StackTrace StackTrace { get; }
    
    public MainMikitanException(){ }
    
    public MainMikitanException(string message) : base(message) { }
    
    public MainMikitanException(string message, Exception innerException) : base(message, innerException) { }

    public MainMikitanException(string message, string className) : base(message)
    {
        ClassName = className;
    }

    public MainMikitanException(string message, string className , Exception innerException) : base(message, innerException)
    {
        ClassName = className;
    }
    
    public MainMikitanException(string message, string className, StackTrace stackTrace) : base(message)
    {
        ClassName = className;
        StackTrace = stackTrace;
    }
    
    public MainMikitanException(string message, string className, StackTrace stackTrace, Exception exception) : base(message, exception)
    {
        ClassName = className;
        StackTrace = stackTrace;
    }
}