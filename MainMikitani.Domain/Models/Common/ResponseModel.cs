namespace MainMikitan.Domain.Models.Commons;

public record ResponseModel<T>
{
    public string? ErrorType { get; set; }
    public string? ErrorMessage { get; set;}
    public T? Result { get; set; }
    public bool HasError
    {
        get
        {
            return !string.IsNullOrEmpty(ErrorType);
        }
    }
}
