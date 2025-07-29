namespace BoostEvents.Web.Domain.ValueObjects;

public sealed class DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public DateRange(DateTime start, DateTime end)
    {
        if (end <= start)
            throw new ArgumentException("End date must be after start date.");

        Start = start;
        End = end;
    }

    public TimeSpan Duration => End - Start;

    public bool Overlaps(DateRange other) =>
        Start < other.End && End > other.Start;

    public override string ToString() => $"{Start:u} to {End:u}";
}