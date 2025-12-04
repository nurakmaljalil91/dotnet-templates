namespace Application.Common.Interfaces;

public interface IDateTime
{
    DateTime Now { get; }    
    DateTimeOffset NowOffset { get; }
    DateTime UtcNow { get; }
}
