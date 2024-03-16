namespace MainMikitan.Domain.Models.Logger;

public class LoggerEntity
{
    public int Id { get; set; }
    public string Request { get; set; } = null!;
    public string Response { get; set; } = null!;
    public string MethodName { get; set; } = null!;
    public string? Exception { get; set; }
    public string? StackTrace { get; set; }
    public string? Data { get; set; }
    public DateTimeOffset ThrowTime { get; set; }
}