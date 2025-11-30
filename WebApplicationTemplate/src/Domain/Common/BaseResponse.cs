#nullable enable
namespace Domain.Common;

/// <summary>
/// Represents a standard response with status code, details, and message.
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// Gets or sets the status code of the response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets additional details about the response.
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Gets or sets a message describing the response.
    /// </summary>
    public string? Message { get; set; }    
}
