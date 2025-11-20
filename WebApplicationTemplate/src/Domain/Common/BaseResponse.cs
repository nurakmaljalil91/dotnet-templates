using Domain.Enums;

namespace Domain.Common;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string? Details { get; set; }
    public string? Message { get; set; }    
}