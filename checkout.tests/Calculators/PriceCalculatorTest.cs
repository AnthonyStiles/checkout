using checkout.Calculators;
using checkout.Data;
using Xunit;

namespace checkout.tests.Calculators;

public class PriceCalculatorTest
{
    [Fact]
    public void PriceCalculator_CalculateItemTotal_NoSpecialPrice_ShouldCalculateTotalPrice()
    {
        ItemPricing itemPricing = new(){ Price = 10, SKU = "A"};

        int totalPrice = PriceCalculator.CalculateItemTotal(itemPricing, 10);
        
        Assert.Equal(100, totalPrice);
    }
    
    [Theory]
    [InlineData(1, 10, 2, 5, 10)] //special price doesn't apply
    [InlineData(2, 10, 2, 5, 5)] //special price applies
    [InlineData(3, 10, 2, 5, 15)] //special price applies with remainder
    [InlineData(50, 10, 10, 5, 25)] //special price applies with larger number
    [InlineData(50, 10, 3, 5, 100)] //special price applies with larger number
    public void PriceCalculator_CalculateItemTotal_SpecialPrice_ShouldCalculateTotalPrice(int itemCount, int itemPrice,
        int specialPriceCount, int specialPrice, int total)
    {
        ItemPricing itemPricing = new(){ Price = 10, SKU = "A", SpecialPrice = new SpecialItemPricing{ Count = specialPriceCount, Price = specialPrice }};

        int calculatedPrice = PriceCalculator.CalculateItemTotal(itemPricing, itemCount);
        
        Assert.Equal(total, calculatedPrice);
    }
}