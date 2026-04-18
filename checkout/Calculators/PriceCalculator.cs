using checkout.Data;

namespace checkout.Calculators;

internal static class PriceCalculator
{
    internal static int CalculateTotalPrice(ItemPricing itemPricing, int count)
    {
        return count * itemPricing.Price;
    }

    internal static int CalculateTotalPrice(ItemPricing itemPricing, SpecialItemPricing specialItemPricing, int count)
    {
        var specialPriceGroups = count / specialItemPricing.Count;
        var totalSpecialPriceAmount = specialPriceGroups * specialItemPricing.Price;

        var remainingItems = count % specialItemPricing.Count;
        var totalRemainingItemsPrice = remainingItems * itemPricing.Price;

        return totalSpecialPriceAmount + totalRemainingItemsPrice;
    }
}