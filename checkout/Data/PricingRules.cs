namespace checkout.Data;

internal class PricingRules
{
    public List<ItemPricing> ItemPrices { get; set; } = [];
    public List<SpecialItemPricing> SpecialItemPrices { get; set; } = [];
}