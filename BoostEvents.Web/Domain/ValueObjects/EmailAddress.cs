namespace BoostEvents.Web.Domain.ValueObjects;

public sealed class EmailAddress
{
    public string Value { get; }

    private EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            throw new ArgumentException("Invalid email address.");

        Value = value;
    }

    public static EmailAddress From(string value) => new(value);

    public override bool Equals(object? obj) =>
        obj is EmailAddress other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}