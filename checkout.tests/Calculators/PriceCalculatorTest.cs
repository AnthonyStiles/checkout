using checkout.Calculators;
using checkout.Data;
using Xunit;

namespace checkout.tests.Calculators;

public class PriceCalculatorTest
{
    [Fact]
    public void PriceCalculator_CalculateItemTotal_NoItems_ShouldReturnZero()
    {
        ItemPricing itemPricing = new() { Price = 10, SKU = "A" };

        var totalPrice = PriceCalculator.CalculateItemTotal(itemPricing, 0);

        Assert.Equal(0, totalPrice);
    }

    [Fact]
    public void PriceCalculator_CalculateItemTotal_NoSpecialPrice_ShouldCalculateTotalPrice()
    {
        ItemPricing itemPricing = new() { Price = 10, SKU = "A" };

        var totalPrice = PriceCalculator.CalculateItemTotal(itemPricing, 10);

        Assert.Equal(100, totalPrice);
    }

    [Fact]
    public void PriceCalculator_CalculateItemTotal_NullItemPricing_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => PriceCalculator.CalculateItemTotal(null!, 10));
    }

    [Theory]
    [InlineData(1, 10, 2, 5, 10)] //special price doesn't apply
    [InlineData(2, 10, 2, 5, 5)] //special price applies
    [InlineData(1, 10, 1, 5, 5)] //special price applies with one item
    [InlineData(3, 10, 2, 5, 15)] //special price applies with remainder
    [InlineData(50, 10, 10, 5, 25)] //special price applies with larger number
    [InlineData(1000, 100, 7, 50, 7700)] //special price applies with even larger number
    [InlineData(3, 10, 2, 0, 10)] //special price applies with 0 price
    public void PriceCalculator_CalculateItemTotal_SpecialPrice_ShouldCalculateTotalPrice(int itemCount, int itemPrice,
        int specialPriceCount, int specialPrice, int total)
    {
        ItemPricing itemPricing = new()
        {
            Price = itemPrice, SKU = "A",
            SpecialPrice = new SpecialItemPricing { Count = specialPriceCount, Price = specialPrice }
        };

        var calculatedPrice = PriceCalculator.CalculateItemTotal(itemPricing, itemCount);

        Assert.Equal(total, calculatedPrice);
    }
}