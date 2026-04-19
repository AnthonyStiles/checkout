using checkout.Calculators;
using checkout.Data;

namespace checkout;

public class Checkout : ICheckout
{
    private readonly PricingRules _pricingRules;
    private readonly Dictionary<string, int> _scannedItems = new();

    public Checkout(PricingRules pricingRules)
    {
        _pricingRules = pricingRules ?? throw new ArgumentNullException(nameof(pricingRules));
    }

    public int GetTotalPrice()
    {
        if (_scannedItems.Count == 0)
        {
            return 0;
        }

        var total = 0;

        foreach (var (item, count) in _scannedItems)
        {
            var itemPricing = _pricingRules.ItemPrices.FirstOrDefault(itemPrice => itemPrice.SKU == item);
            total += PriceCalculator.CalculateItemTotal(itemPricing, count);
        }

        return total;
    }

    public void Scan(string item)
    {
        ValidateScannedItem(item);

        if (!_scannedItems.TryAdd(item, 1))
        {
            _scannedItems[item]++;
        }
    }

    private void ValidateScannedItem(string item)
    {
        if (string.IsNullOrWhiteSpace(item))
        {
            throw new ArgumentException("item is required.", nameof(item));
        }

        if (_pricingRules.ItemPrices.All(itemPrice => itemPrice.SKU != item))
        {
            throw new ArgumentException("item doesn't have pricing information.", nameof(item));
        }
    }
}