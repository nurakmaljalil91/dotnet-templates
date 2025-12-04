using NodaTime;
using NodaTime.Text;

namespace Application.Common.Interfaces;

public interface IClockService
{
    DateTimeZone TimeZone { get; }

    Instant Now { get; }

    LocalDateTime LocalNow { get; }
    
    LocalDate Today { get; }

    Instant ToInstant(LocalDateTime local);

    LocalDateTime ToLocal(Instant instant);

    ParseResult<LocalDate>? TryParseDate(string? date);
    
    ParseResult<LocalDateTime>? TryParseDateTime(string? time);
}