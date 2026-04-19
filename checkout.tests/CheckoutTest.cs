using checkout.Data;
using Xunit;

namespace checkout.tests;

public class CheckoutTest
{
    private readonly PricingRules _pricingRules = new()
    {
        ItemPrices =
        [
            new ItemPricing
            {
                Price = 10,
                SKU = "A"
            },
            new ItemPricing
            {
                Price = 20,
                SKU = "B",
                SpecialPrice = new SpecialItemPricing
                {
                    Count = 2,
                    Price = 10
                }
            }
        ]
    };

    [Fact]
    public void Checkout_Constructor_NullPricingRules_ShouldThrowArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Checkout(null!));
    }

    [Fact]
    public void Checkout_GetTotalPrice_MultipleItems_ShouldReturnCorrectValue()
    {
        var checkout = new Checkout(_pricingRules);
        checkout.Scan("A");
        checkout.Scan("A");
        var total = checkout.GetTotalPrice();
        Assert.Equal(20, total);
    }

    [Fact]
    public void Checkout_GetTotalPrice_MultipleItemsWithSpecialPrice_ShouldReturnCorrectValue()
    {
        var checkout = new Checkout(_pricingRules);
        checkout.Scan("A");
        checkout.Scan("B");
        checkout.Scan("B");
        var total = checkout.GetTotalPrice();
        Assert.Equal(20, total);
    }
    
    [Fact]
    public void Checkout_GetTotalPrice_ZeroScannedItems_ShouldReturnCorrectValue()
    {
        var checkout = new Checkout(_pricingRules);
        var total = checkout.GetTotalPrice();
        Assert.Equal(0, total);
    }

    [Fact]
    public void Checkout_GetTotalPrice_SingleItem_ShouldReturnCorrectValue()
    {
        var checkout = new Checkout(_pricingRules);
        checkout.Scan("A");
        var total = checkout.GetTotalPrice();
        Assert.Equal(10, total);
    }

    [Fact]
    public void Checkout_Scan_MultipleValidItems_ShouldSucceed()
    {
        var checkout = new Checkout(_pricingRules);
        checkout.Scan("A");
        checkout.Scan("A");
    }

    [Fact]
    public void Checkout_Scan_NonExistingItem_ShouldThrowArgumentException()
    {
        var checkout = new Checkout(_pricingRules);
        Assert.Throws<ArgumentException>(() => checkout.Scan("C"));
    }

    [Fact]
    public void Checkout_Scan_NullOrWhiteSpaceItem_ShouldThrowArgumentException()
    {
        var checkout = new Checkout(_pricingRules);
        Assert.Throws<ArgumentException>(() => checkout.Scan(null!));
        Assert.Throws<ArgumentException>(() => checkout.Scan(string.Empty));
        Assert.Throws<ArgumentException>(() => checkout.Scan(" "));
    }

    [Fact]
    public void Checkout_Scan_ValidItem_ShouldSucceed()
    {
        var checkout = new Checkout(_pricingRules);
        checkout.Scan("A");
    }
}