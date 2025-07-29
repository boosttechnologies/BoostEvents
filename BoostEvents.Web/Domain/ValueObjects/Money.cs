namespace BoostEvents.Web.Domain.ValueObjects;

public readonly struct Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.");

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public override string ToString() => $"{Currency} {Amount:N2}";
}