namespace Domain;
public class CommonResponse
{
    public object? Data { get; set; } 
    public string Message { get; set; } = default!;
    public bool IsSuccess { get; set; }
}
