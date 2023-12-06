namespace MainMikitan.Domain.Models.Commons;

public class ResponseMetaData<T>
{
    public string? Version { get; set; }
    public string? ErrorResponse { get; set; }
    public T? Result { get; set; }
}
