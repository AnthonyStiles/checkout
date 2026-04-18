namespace checkout;

internal interface ICheckout
{
    int GetTotalPrice();
    void Scan(string item);
}