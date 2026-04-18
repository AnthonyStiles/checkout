namespace checkout.Data;

public class ItemPricing
{
    public int Price { get; set; }
    public required string SKU { get; set; }
    
    public SpecialItemPricing? SpecialPrice { get; set; }
}