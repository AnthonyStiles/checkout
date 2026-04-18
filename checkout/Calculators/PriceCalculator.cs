using checkout.Data;

namespace checkout.Calculators;

public static class PriceCalculator
{
    public static int CalculateItemTotal(ItemPricing itemPricing, int count)
    {
        if (itemPricing.SpecialPrice is not null)
        {
            var specialPriceGroups = count / itemPricing.SpecialPrice.Count;
            var totalSpecialPriceAmount = specialPriceGroups * itemPricing.SpecialPrice.Price;

            var remainingItems = count % itemPricing.SpecialPrice.Count;
            var totalRemainingItemsPrice = remainingItems * itemPricing.Price;

            return totalSpecialPriceAmount + totalRemainingItemsPrice;
        }

        return count * itemPricing.Price;
    }
}